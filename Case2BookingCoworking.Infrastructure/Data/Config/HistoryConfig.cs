using Case2BookingCoworking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Case2BookingCoworking.Infrastructure.Data.Config
{
    internal class HistoryConfig : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("History");
            builder.HasOne(h => h.User);
            builder.HasOne(h => h.Audience);
        }
    }
}
