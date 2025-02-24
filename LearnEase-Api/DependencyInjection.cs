﻿using LearnEase.Repository.UOW;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Core.Services;
using LearnEase_Api.LearnEase.Infrastructure;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using LearnEase_Api.LearnEase.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace LearnEase_Api
{
    public static class DependencyInjection
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddDatabase(configuration);
            services.AddRedis(configuration);
            services.AddCors();
            services.AddLogging();
        }

        // SQL Server configuration
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        // Services
        public static void AddServices(this IServiceCollection services)
        {
            //Service
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthService, AuthService>()
                .AddSingleton<IRedisCacheService, RedisCacheService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IUserDetailService, UserDetailService>();

            //Repo
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRoleRepository, RolesRepository>()
                .AddScoped<IUserDetailRepository, UserDetailRepository>();

        }

        // Redis Cloud
        public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConfig = configuration.GetSection("RedisString");

            var options = new ConfigurationOptions
            {
                EndPoints = { { redisConfig["Redis"], redisConfig.GetValue<int>("Port") } },
                User = redisConfig["Username"],
                Password = redisConfig["Password"]
            };

            var muxer = ConnectionMultiplexer.Connect(options);

            services.AddSingleton<IConnectionMultiplexer>(muxer);
        }

        // CORS policy
        public static void AddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

        }

        // Logging configuration
        public static void AddLogging(this IServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddConsole();
                config.AddDebug();
            });
        }

    }
}
