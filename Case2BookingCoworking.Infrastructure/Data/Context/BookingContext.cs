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
        public DbSet<User> Users { get; set; }
        public DbSet<Audience> Audiences { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public BookingContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public BookingContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
