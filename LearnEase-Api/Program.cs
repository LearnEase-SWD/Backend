using System.Net;
using LearnEase_Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// Load Configurations
builder.Services.AddConfig(builder.Configuration);

var app = builder.Build();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Tạm thời comment Middleware tùy chỉnh để kiểm tra lỗi
// app.UseMiddleware<TokenValidationMiddleware>();

app.MapControllers();
app.Run();
