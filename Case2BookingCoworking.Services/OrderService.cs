using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Application.Abstract.Services;
using Case2BookingCoworking.Contracts.Requests;
using Case2BookingCoworking.Contracts.Responses;
using Case2BookingCoworking.Core.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2BookingCoworking.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepos _orderRepos;
        private readonly IAudienceService _audienceService;
        private readonly IAudienceRepos _audienceRepos;
        private readonly IUserRepos _userRepos;
        public OrderService(IAudienceService audienceService, IOrderRepos orderRepos, IAudienceRepos audience, IUserRepos userRepos) 
        {
            _orderRepos = orderRepos;
            _audienceService = audienceService;
            _audienceRepos = audience;
            _userRepos = userRepos;
        }
        public async Task<ErrorOr<Success>> CreateOrder(OrderAudienceRequest orderRequest, Guid userId, CancellationToken cancellationToken)
        {
            var checkAud = await _audienceService.CheckBookAudience(orderRequest, userId, cancellationToken);
            if (checkAud.Value == Result.Success)
            {
                var order = new Order();
                var audience = await _audienceRepos.GetAudienceByNumberAsync(orderRequest.audienceNumber, cancellationToken);
                order.AudienceNumber = orderRequest.audienceNumber;
                order.StartOfBooking = DateTime.Parse(orderRequest.startOfBooking);
                order.EndOfBooking = DateTime.Parse(orderRequest.endOfBooking);
                if (audience.IsError)
                    return audience.Errors;
                order.Audience = audience.Value;
                var user = await _userRepos.GetByIdAsync(userId, cancellationToken);
                order.User = user.Value;

                var result = await _orderRepos.AddAsync(order, cancellationToken);
                return result;

            }
            return Error.Conflict("no availbe audiences");
        }

        public async Task<ErrorOr<List<OrderResponse>>> GetAllOrders(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
