using Case2BookingCoworking.Application.Abstract.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Infrastructure.Email.Verification.Depends
{
    public class Verification5DigitCodeGenerator : IVerificationCodeGenerator
    {
        public string Generate()
        {
            var random = new Random();

            int code = random.Next(10000, 99999);

            return code.ToString();
        }
    }
}
