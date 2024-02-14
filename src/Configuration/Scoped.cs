using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;
using TrainingRestFullApi.src.Entities;
using TrainingRestFullApi.src.Interfaces;
using TrainingRestFullApi.src.Middleware;
using TrainingRestFullApi.src.Service;
using TrainingRestFullApi.src.Utils;

namespace TrainingRestFullApi.src.Configuration
{
    public class Scoped
    {
        public void AddScopedService(IServiceCollection service) {
            // MiddleWare
            service.AddScoped<JwtMiddleWare>();
            // Utils
            service.AddScoped<HashPassword>();
            service.AddScoped<RolesService>();
            // Auto Mapper
            service.AddAutoMapper(typeof(MappingProfile));
            // Services
            service.AddScoped<IUserAccount, UserService>();
            service.AddScoped<IMovie, MovieService>();
            service.AddScoped<IReview, ReviewService>();
            service.AddScoped<IAuthorizationHandler, CustomRoleAuthorizationHandler>();
            service.AddScoped<IRating, RatingService>();
            // HTTP
            service.AddHttpContextAccessor();
            // JSon
            service.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


        }
        public void AddAuthenticationJwt(IServiceCollection service, IConfiguration builder)
        {
            // JWT Generate Options
            service.AddAuthentication(auth =>
            {
                    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                string? secretKey = builder["Jwt:SecretKey"];
                string? issuer = builder["Jwt:Issuer"];
                string? audience = builder["Jwt:Audience"];
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            // SwaggerOptions
            service.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            // Authorization
            service.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.Requirements.Add(new CustomRoleRequirement("Admin")));
                options.AddPolicy("User", policy => policy.Requirements.Add(new CustomRoleRequirement("User")));

            });
        }
    }
}
