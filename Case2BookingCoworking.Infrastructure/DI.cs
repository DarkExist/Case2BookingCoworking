using Case2BookingCoworking.Application.Abstract;
using Case2BookingCoworking.Application.Abstract.Email;
using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Infrastructure.Data.Context;
using Case2BookingCoworking.Infrastructure.Data.Repos;
using Case2BookingCoworking.Infrastructure.Email.Verification.Depends;
using DODQuiz.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using Case2BookingCoworking.Infrastructure.Email.Verification;

namespace Case2BookingCoworking.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IJWTProvider, JWTProvider>();

            return services;
        }
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddDbContext<BookingContext>();

            services.AddScoped<IUserRepos, UserRepos>();
            services.AddScoped<IProfileRepos, ProfileRepos>();
            services.AddScoped<IRoleRepos, RoleRepos>();
            services.AddScoped<IAudienceRepos, AudienceRepos>();
            services.AddScoped<IHistoryRepos, HistoryRepos>();
            services.AddScoped<IOrderRepos, OrderRepos>();

            return services;
        }
        public static IServiceCollection AddEmailVerification(this IServiceCollection services)
        {
            services.AddTransient<IEmailVerificationService, EmailVerificationService>();
            services.AddTransient<IVerificationCodeGenerator, Verification5DigitCodeGenerator>();
            services.AddTransient<IConcurrentVerificationCodeStorage, ConcurrentVerificationCodeStorage>();
            services.AddTransient<ICodeVerificationService, CodeVerificationService>();

            return services;
        }
    }
}
