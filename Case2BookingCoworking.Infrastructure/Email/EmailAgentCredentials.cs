using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Infrastructure.Email
{
    public class EmailAgentCredentials
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public EmailAgentCredentials(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
