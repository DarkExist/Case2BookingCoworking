using Case2BookingCoworking.Contracts.Responses;
using ErrorOr;

namespace Case2BookingCoworking.Services
{
    public interface IAudienceService
    {
        Task<ErrorOr<AudienceAvailableResponse>> AddNewAudience();
        Task<ErrorOr<Success>> BookAudience();
        Task<ErrorOr<Success>> CancelBooking();
        Task<ErrorOr<List<AudienceAvailableResponse>>> GetAvailableAudiences();
        Task<ErrorOr<List<AudienceAvailableResponse>>> GetBookedAudiences();
        Task<ErrorOr<Success>> RemoveAudience();
        Task<ErrorOr<Success>> UpdateBookedAudiences();
    }
}