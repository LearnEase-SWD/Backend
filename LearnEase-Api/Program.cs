using LearnEase_Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// Load Configurations
builder.Services.AddConfig(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// ✅ Đảm bảo Authentication chạy trước Middleware tùy chỉnh
app.UseAuthentication();
app.UseAuthorization();

// ❌ Tạm thời comment Middleware tùy chỉnh để kiểm tra lỗi
// app.UseMiddleware<TokenValidationMiddleware>();

app.MapControllers();
app.Run();
