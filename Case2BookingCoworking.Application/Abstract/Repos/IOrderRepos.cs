using Case2BookingCoworking.Core.Domain.Entities;
using ErrorOr;

namespace Case2BookingCoworking.Application.Abstract.Repos
{
    public interface IOrderRepos : IRepos<Order>
    {
		public Task<ErrorOr<List<Order>>> GetOrdersByNumberAsync(string number);

	}
}
