using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Contracts.Requests
{
	public record OrderAudienceRequest (
		string audienceNumber,
		int amountOfStudents,
		DateTime startOfBooking,
		DateTime endOfBooking
	);
}
