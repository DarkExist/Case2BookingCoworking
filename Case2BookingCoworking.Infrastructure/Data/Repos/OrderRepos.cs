using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Core.Domain.Entities;
using Case2BookingCoworking.Infrastructure.Data.Context;
using Case2BookingCoworking.Infrastructure.Errors;
using CSharpFunctionalExtensions;
using ErrorOr;

namespace Case2BookingCoworking.Infrastructure.Data.Repos
{
    internal class OrderRepos : BaseRepository<Order>, IOrderRepos
    {

        public OrderRepos(BookingContext context) : base(context)
        {
        }

        public async Task<ErrorOr<List<Order>>> GetOrdersByNumberAsync(string number)
        {
            try
            {
                return _context.Orders.Where(o => o.AudienceNumber == number).ToList();
            }
			catch (OperationCanceledException)
			{
				return new TokenCancelledErrorBuilder().BuildError();
			}
			catch (Exception ex)
			{
                return Error.Failure("DB is not available");
			}
		} 
    }
}
