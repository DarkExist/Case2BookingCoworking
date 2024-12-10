using CSharpFunctionalExtensions;

namespace Case2BookingCoworking.Core.Domain.Entities
{
    public class Audience : Entity<Guid>
    {
        public Audience() { Id = Guid.NewGuid(); }
        public string Number { get; set; }
        public int Capacity { get; set; }
        public List<AudienceType> Type { get; set; } = new List<AudienceType>();

        public List<Order> Orders { get; set; }

        public string Status { get; set; }
    }
}
