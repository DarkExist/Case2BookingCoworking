using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Application.Abstract.Email
{
    public class VerificationCodeDetails
    {
        public string Code { get; set; }
        public DateTime SendingTime { get; set; }

        public VerificationCodeDetails(string code, DateTime sendingTime)
        {
            Code = code;
            SendingTime = sendingTime;
        }
    }
}
