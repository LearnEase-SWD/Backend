﻿using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
using LearnEase.Repository.Repository;
using LearnEase.Repository.UOW;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Core.Services;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using LearnEase_Api.LearnEase.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
			services.AddSwagger();
			services.AddAuthen(configuration);
		}

		// Swagger
		public static void AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Nhập ID Token của Google (Bearer YOUR_TOKEN)",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			});
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
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddSingleton<IRedisCacheService, RedisCacheService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IUserDetailService, UserDetailService>();
			services.AddScoped<ICourseService, CourseService>();
			services.AddScoped<IExerciseService, ExerciseService>();
			services.AddScoped<IFlashcardService, FlashcardService>();

            //Repo
            services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleRepository, RolesRepository>();
			services.AddScoped<IUserDetailRepository, UserDetailRepository>();
			services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
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

		// JWT Authentication
		/*public static void AddAuthen(this IServiceCollection services, IConfiguration configuration)
        {
            var googleClientId = configuration["Authentication:Google:ClientId"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://accounts.google.com";
                options.Audience = googleClientId;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://accounts.google.com",
                    ValidateAudience = true,
                    ValidAudience = googleClientId,
                    ValidateLifetime = true
                };
            });

            services.AddAuthorization();
        }*/

		public static void AddAuthen(this IServiceCollection services, IConfiguration configuration)
		{
			var googleClientId = configuration["Authentication:Google:ClientId"];
			var googleClientSecret = configuration["Authentication:Google:ClientSecret"];

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
			})
			.AddCookie()
			.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
			{
				options.ClientId = googleClientId;
				options.ClientSecret = googleClientSecret;
				options.CallbackPath = "/callback";

				options.SaveTokens = true;

				options.Scope.Add("email");
				options.Scope.Add("profile");
				options.Scope.Add("openid");
			});

			services.AddAuthorization();
		}
	}
}
