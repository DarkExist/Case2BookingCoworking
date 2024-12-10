using Case2BookingCoworking.Application.Abstract;
using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Infrastructure.Data.Context;
using Case2BookingCoworking.Infrastructure.Data.Repos;
using DODQuiz.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
