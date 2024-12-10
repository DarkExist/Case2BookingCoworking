using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Application.Abstract.Email
{
    public interface IVerificationCodeGenerator
    {
        string Generate();
    }
}
