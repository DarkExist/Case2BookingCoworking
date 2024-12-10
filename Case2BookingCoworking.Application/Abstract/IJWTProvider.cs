using Case2BookingCoworking.Core.Domain.Entities;

namespace Case2BookingCoworking.Application.Abstract
{
    public interface IJWTProvider
    {
        string GenerateToken(User user);
    }
}
