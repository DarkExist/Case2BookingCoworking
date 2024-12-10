using Case2BookingCoworking.Contracts.Requests;
using Case2BookingCoworking.Contracts.Responses;
using ErrorOr;

namespace Case2BookingCoworking.Services
{
	public interface IAudienceService
	{
		Task<ErrorOr<Success>> AddNewAudience(CreateAudienceRequest createAudienceRequest, CancellationToken cancellationToken);
		Task<ErrorOr<Success>> CheckBookAudience(OrderAudienceRequest audienceRequest, Guid userId, CancellationToken cancellationToken);
		Task<ErrorOr<Success>> CancelBooking(Guid orderId, CancellationToken cancellationToken);
		Task<ErrorOr<AudienceAvailableResponse>> GetAvailableAudiences(CancellationToken cancellationToken);
		Task<ErrorOr<AudienceBookedResponse>> GetBookedAudiences(CancellationToken cancellationToken);
		Task<ErrorOr<Success>> RemoveAudience(Guid audienceId, CancellationToken cancellationToken);
		Task<ErrorOr<Success>> UpdateBookedAudiences(Guid audienceId, CancellationToken cancellationToken);
	}
}