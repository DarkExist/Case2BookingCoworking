using Case2BookingCoworking.Contracts.Requests;
using Case2BookingCoworking.Contracts.Responses;
using ErrorOr;

namespace Case2BookingCoworking.Application.Abstract.Services { 
	public interface IAudienceService
	{
		Task<ErrorOr<AudienceAvailableResponse>> AddNewAudience();
		Task<ErrorOr<Success>> BookAudience(AudienceRequest audienceRequest, Guid userId, CancellationToken cancellationToken);
		Task<ErrorOr<Success>> CancelBooking();
		Task<ErrorOr<List<AudienceAvailableResponse>>> GetAvailableAudiences();
		Task<ErrorOr<AudienceAvailableResponse>> GetAvailableAudiences(CancellationToken cancellationToken);
		Task<ErrorOr<List<AudienceAvailableResponse>>> GetBookedAudiences();
		Task<ErrorOr<AudienceBookedResponce>> GetBookedAudiences(CancellationToken cancellationToken);
		Task<ErrorOr<Success>> RemoveAudience();
		Task<ErrorOr<Success>> UpdateBookedAudiences();
	}
}