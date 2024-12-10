using CSharpFunctionalExtensions;

namespace Case2BookingCoworking.Core.Domain.Entities
{
    public class Audience : Entity<Guid>
    {
        public string Number { get; set; }
        public int Capacity { get; set; }
        public AudienceType Type { get; set; }

        public List<Order> Orders { get; set; }

        public string Status { get; set; }
    }
}
