using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Application
{
	public class BookedAudiencesObject : Dictionary<string, List<Tuple<DateTime, DateTime>>>
	{
		public BookedAudiencesObject() : base() { }
	}
}
