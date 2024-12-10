using Case2BookingCoworking.Contracts.Responses;
using ErrorOr;

namespace Case2BookingCoworking.Services
{
    internal class AudienceService : IAudienceService
    {
        public Task<ErrorOr<Success>> BookAudience() { throw new NotImplementedException(); }
        public Task<ErrorOr<List<AudienceResponse>>> GetAvailableAudiences() { throw new NotImplementedException(); }
        public Task<ErrorOr<List<AudienceResponse>>> GetBookedAudiences() { throw new NotImplementedException(); }
        public Task<ErrorOr<Success>> CancelBooking() { throw new NotImplementedException(); }

        public Task<ErrorOr<Success>> UpdateBookedAudiences() { throw new NotImplementedException(); }
        public Task<ErrorOr<AudienceResponse>> AddNewAudience() { throw new NotImplementedException(); }
        public Task<ErrorOr<Success>> RemoveAudience() { throw new NotImplementedException(); }
    }
}
