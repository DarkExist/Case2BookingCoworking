using Case2BookingCoworking.Core.Domain.Entities;
using Case2BookingCoworking.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Case2BookingCoworking.Infrastructure.Data.Context
{
    public class BookingContext : DbContext
    {
        protected readonly IConfiguration? _configuration;
        DbSet<User> Users;
        DbSet<Audience> Audiences;
        DbSet<Order> Orders;
        DbSet<Role> Roles;
        DbSet<Profile> Profiles;

        public BookingContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public BookingContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var options = _configuration?.GetRequiredSection("ConnectionStrings");
            optionsBuilder.UseNpgsql(options?.GetRequiredSection("BookingApp").Value ?? "server=localhost, username=pgdamin")
                .UseLoggerFactory(CreateLoggerFactory())
                .EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AudienceConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new ProfileConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new HistoryConfig());
        }
            public ILoggerFactory CreateLoggerFactory() => LoggerFactory
            .Create(builder => { builder.AddConsole(); });
    }
}
