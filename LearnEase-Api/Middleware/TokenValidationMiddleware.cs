using LearnEase_Api.LearnEase.Core.IServices;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRedisCacheService _redisCacheService;

    public TokenValidationMiddleware(RequestDelegate next, IRedisCacheService redisCacheService)
    {
        _next = next;
        _redisCacheService = redisCacheService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
     
        if (context.Request.Path.StartsWithSegments("/api/auth/login") ||
            context.Request.Path.StartsWithSegments("/api/auth/callback") ||
            context.Request.Path.StartsWithSegments("/callback"))
        {

            await _next(context);
            return;
        }

 
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    
            var cachedToken = await _redisCacheService.GetAsync<string>(token);
            if (string.IsNullOrEmpty(cachedToken)) 
            {
                context.Response.StatusCode = 401; 
                await context.Response.WriteAsync("Unauthorized: Invalid or expired token");
                return;
            }

            
            var tokenExpiration = await _redisCacheService.GetAsync<DateTime?>($"expiration_{token}");
            if (tokenExpiration == null || tokenExpiration <= DateTime.UtcNow) 
            {
                context.Response.StatusCode = 401; 
                await context.Response.WriteAsync("Unauthorized: Token has expired");
                return;
            }

            
            if (tokenExpiration.Value.Subtract(DateTime.UtcNow).TotalMinutes < 10) 
            {
               
                var newExpirationTime = DateTime.UtcNow.AddMinutes(60); 
                await _redisCacheService.SetAsync($"expiration_{token}", newExpirationTime, TimeSpan.FromMinutes(60));
            }

            
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Authorization header is missing");
        }
    }
}
