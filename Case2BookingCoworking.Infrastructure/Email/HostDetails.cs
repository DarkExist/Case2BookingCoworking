using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Infrastructure.Email
{
    public class HostDetails
    {
        public string Name { get; set; }
        public int Port { get; set; }

        public HostDetails(string hostName, int hostPort)
        {
            Name = hostName;
            Port = hostPort;
        }
    }
}
