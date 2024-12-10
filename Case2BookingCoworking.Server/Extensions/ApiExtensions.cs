using Case2BookingCoworking.Application.Abstract.Services;
using Case2BookingCoworking.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Case2BookingCoworking.Server.Extensions
{
    public static class ApiExtensions
    {
        public static void AddBookingServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAudienceService, AudienceService>();
            services.AddTransient<IOrderService, OrderService>();
        }
        public static void AddApiAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtoptions = configuration.GetRequiredSection("jwtoptions");
            var secretKey = jwtoptions.GetRequiredSection("SecretKey").Value;
            var key = Encoding.UTF8.GetBytes(secretKey);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["bivis-bober"];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(

                options =>
                {
                    options.AddPolicy("admin",
                        policy =>
                        {
                            policy.RequireRole("ADMIN");

                        });

                    options.AddPolicy("user",
                        policy =>
                        {
                            policy.RequireRole("ADMIN", "USER","TEACHER");

                        });
                    options.AddPolicy("teacher",
                        policy =>
                        {
                            policy.RequireRole("ADMIN", "TEACHER");
                        }
                        );
                }
             );
        }
    }
}
