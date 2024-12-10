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
		public User user;
		public Audience audience;
		public Guid userId;
		public string audienceNumber;

	}
}
