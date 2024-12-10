using CSharpFunctionalExtensions;

namespace Case2BookingCoworking.Core.Domain.Entities
{
    public class Profile : Entity<Guid>
    {
        public Profile() { 
            Id = Guid.NewGuid(); 
            FullCredential = string.Empty;
            TelegramId = string.Empty;
            PhoneNumber = string.Empty;
            UniversityEmail = string.Empty;
        }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string FullCredential { get; set; }
        public string TelegramId { get; set; }
        public string PhoneNumber { get; set; }
        public string UniversityEmail { get; set; }
    }
}
