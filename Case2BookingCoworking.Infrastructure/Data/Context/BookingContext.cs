using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Case2BookingCoworking.Infrastructure.Data.Context
{
    public class BookingContext : DbContext
    {
        protected readonly IConfiguration? _configuration;

        public BookingContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var options = _configuration?.GetRequiredSection("ConnectionStrings");
            optionsBuilder.UseNpgsql(options?.GetRequiredSection("BookingApp").Value ?? "server=localhost, username=pgdamin")
                .UseLoggerFactory(CreateLoggerFactory())
                .EnableSensitiveDataLogging();
        }
        public ILoggerFactory CreateLoggerFactory() => LoggerFactory
            .Create(builder => { builder.AddConsole(); });
    }
}
