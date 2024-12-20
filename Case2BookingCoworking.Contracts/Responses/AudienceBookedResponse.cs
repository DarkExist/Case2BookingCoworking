﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Contracts.Responses
{
	public record AudienceBookedResponse
	(
		Dictionary<string, List<Tuple<DateTime, DateTime>>> bookedAudiences
		);
}
