using CSharpFunctionalExtensions;

namespace Case2BookingCoworking.Core.Domain.Entities
{
	public class User : Entity<Guid>
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public Profile Profile { get; set; }
		public List<Role> Roles { get; set; }

		public List<Order> Orders { get; set; }
	}
}
