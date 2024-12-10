using Case2BookingCoworking.Contracts.Responses;
using ErrorOr;

namespace Case2BookingCoworking.Services
{
    public interface IAudienceService
    {
        Task<ErrorOr<AudienceResponse>> AddNewAudience();
        Task<ErrorOr<Success>> BookAudience();
        Task<ErrorOr<Success>> CancelBooking();
        Task<ErrorOr<List<AudienceResponse>>> GetAvailableAudiences();
        Task<ErrorOr<List<AudienceResponse>>> GetBookedAudiences();
        Task<ErrorOr<Success>> RemoveAudience();
        Task<ErrorOr<Success>> UpdateBookedAudiences();
    }
}