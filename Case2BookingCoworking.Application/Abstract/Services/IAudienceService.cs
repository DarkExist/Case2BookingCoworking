using Case2BookingCoworking.Contracts.Requests;
using Case2BookingCoworking.Contracts.Responses;
using ErrorOr;

namespace Case2BookingCoworking.Services
{
	public interface IAudienceService
	{
		Task<ErrorOr<Success>> AddNewAudience(List<string> categories, string number, int capacity, CancellationToken cancellationToken);
		Task<ErrorOr<Success>> BookAudience(AudienceRequest audienceRequest, Guid userId, CancellationToken cancellationToken);
		Task<ErrorOr<Success>> CancelBooking(Guid orderId, CancellationToken cancellationToken);
		Task<ErrorOr<AudienceAvailableResponse>> GetAvailableAudiences(CancellationToken cancellationToken);
		Task<ErrorOr<AudienceBookedResponce>> GetBookedAudiences(CancellationToken cancellationToken);
		Task<ErrorOr<Success>> RemoveAudience();
		Task<ErrorOr<Success>> UpdateBookedAudiences();
	}
}