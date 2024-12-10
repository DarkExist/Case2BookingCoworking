using Case2BookingCoworking.Contracts.Requests;
using Case2BookingCoworking.Contracts.Responses;
using ErrorOr;

namespace Case2BookingCoworking.Application.Abstract.Services
{
    public interface IOrderService
    {
        public Task<ErrorOr<Success>> CreateOrder(OrderAudienceRequest orderRequest, Guid userId, CancellationToken cancellationToken);
        public Task<ErrorOr<List<OrderResponse>>> GetAllOrders(CancellationToken cancellationToken);
    }
}
