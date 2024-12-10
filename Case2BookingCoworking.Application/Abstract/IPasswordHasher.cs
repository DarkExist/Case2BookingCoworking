
namespace Case2BookingCoworking.Application.Abstract
{
    public interface IPasswordHasher
    {
        string Generate(string password);

        bool VerifyPassword(string password, string HashedPassword);
    }
}
