using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Core.Domain.Entities
{
	public class Order : Entity<Guid>
	{
		public User User;
		public Audience Audience;
		public Guid UserId;
		public string AudienceNumber;

	}
}
