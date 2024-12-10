using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Core.Domain.Entities
{
	public class Audience : Entity<Guid>
	{
		public string Number {  get; set; }
		public int Capacity { get; set; }
		public AudienceType Type { get; set; }
		public string Status { get; set; }
	}
}
