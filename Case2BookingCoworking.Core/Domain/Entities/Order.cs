using CSharpFunctionalExtensions;

namespace Case2BookingCoworking.Core.Domain.Entities
{
    public class Order : Entity<Guid>
    {
        public User User { get; set; }
        public Audience Audience { get; set; }
        public Guid UserId { get; set; }
        public string AudienceNumber { get; set; }
        public DateTime StartOfBooking { get; set; }
        public DateTime EndOfBooking { get; set; }
    }
}
