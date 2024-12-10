using Case2BookingCoworking.Application;
using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Contracts.Requests;
using Case2BookingCoworking.Contracts.Responses;
using Case2BookingCoworking.Core.Domain.Entities;
using ErrorOr;
using System.Threading;
using Case2BookingCoworking.Application.Abstract.Services;

namespace Case2BookingCoworking.Services
{
	public class AudienceService : IAudienceService
	{
		private readonly IAudienceRepos _audienceRepos;
		private readonly IOrderRepos _orderRepos;
		private readonly IUserRepos _userRepos;
		private readonly IHistoryRepos _historyRepos;

		public AudienceService(IAudienceRepos audienceRepos, IOrderRepos orderRepos,
			IUserRepos userRepos, IHistoryRepos historyRepos)
		{
			_audienceRepos = audienceRepos;
			_orderRepos = orderRepos;
			_userRepos = userRepos;
			_historyRepos = historyRepos;
		}

		public async Task<ErrorOr<Success>> CheckBookAudience(OrderAudienceRequest audienceRequest, Guid userId,
			CancellationToken cancellationToken)
		{
			string audienceNumber = audienceRequest.audienceNumber;
			int amountOfStudents = audienceRequest.amountOfStudents;
			DateTime startOfBooking = audienceRequest.startOfBooking;
			DateTime endOfBooking = audienceRequest.endOfBooking;

			if ((endOfBooking - startOfBooking).TotalMinutes < 5)
			{
				throw new ArgumentException("Too low time interval");
			}

			var errorOrBool = await _IsIntervalAvailable(audienceNumber, startOfBooking, endOfBooking, cancellationToken);

			if (errorOrBool.IsError)
			{
				return errorOrBool.Errors;
			}

			bool availableFlag = errorOrBool.Value;

			if (availableFlag == false)
			{
				return Error.Conflict("This interval already taken");
			}

			var errorOrUser = await _userRepos.GetByIdAsync(userId, cancellationToken);

			if (errorOrUser.IsError)
			{
				return errorOrUser.Errors;
			}

			User user = errorOrUser.Value;

			var errorOrAudience = await _audienceRepos.GetAudienceByNumberAsync(audienceNumber);
			if (errorOrAudience.IsError)
			{
				return errorOrAudience.Errors;
			}

			Audience audience = errorOrAudience.Value;

			Order newOrder = new();
			newOrder.Audience = audience;
			newOrder.AudienceNumber = audience.Number;
			newOrder.UserId = user.Id;
			newOrder.User = user;
			newOrder.StartOfBooking = startOfBooking;
			newOrder.EndOfBooking = endOfBooking;

			var result = await _orderRepos.AddAsync(newOrder, cancellationToken);
			if (result.IsError)
			{
				result = result.Errors;
			}
			return result.Value;
		}
		public async Task<ErrorOr<AudienceAvailableResponse>> GetAvailableAudiences(CancellationToken cancellationToken)
		{
			var errorOrListAudiences = await _audienceRepos.GetAllAsync(cancellationToken);
			if (errorOrListAudiences.IsError)
				return errorOrListAudiences.Errors;

			List<string> numbersOfAudiences = new();

			foreach (var item in errorOrListAudiences.Value)
			{
				if (item.Status != "WORKING")
				{
					numbersOfAudiences.Add(item.Number);
				}
			}
			AudienceAvailableResponse responce = new AudienceAvailableResponse(numbersOfAudiences);
			return responce;
		}
		public async Task<ErrorOr<AudienceBookedResponse>> GetBookedAudiences(CancellationToken cancellationToken)
		{
			var errorOrListAudiences = await GetAvailableAudiences(cancellationToken);
			if (errorOrListAudiences.IsError)
				return errorOrListAudiences.Errors;

			BookedAudiencesObject bookedAudiencesObject = new();

			foreach (var item in errorOrListAudiences.Value.availableAudienceNumber)
			{
				bookedAudiencesObject.Add(item, new());
				var errorOrOrders = await _orderRepos.GetOrdersByNumberAsync(item);

				if (errorOrOrders.IsError)
				{
					return errorOrOrders.Errors;
				}

				foreach (var order in errorOrOrders.Value)
				{
					DateTime startOfBooking = order.StartOfBooking;
					DateTime endOfBooking = order.EndOfBooking;
					bookedAudiencesObject[item].Add(Tuple.Create(startOfBooking, endOfBooking));
				}
			}

			return new AudienceBookedResponse(bookedAudiencesObject);
		}
		public async Task<ErrorOr<Success>> CancelBooking(Guid orderId, CancellationToken cancellationToken)
		{
			var errorOrOrder = await _orderRepos.GetByIdAsync(orderId, cancellationToken);
			if (errorOrOrder.IsError) { return errorOrOrder.Errors; }
			Order order = errorOrOrder.Value;

			History historyOrder = new History();
			historyOrder.User = order.User;
			historyOrder.Audience = order.Audience;
			historyOrder.UserId = order.UserId;
			historyOrder.AudienceNumber = order.AudienceNumber;
			historyOrder.Status = "Закрыт";
			var result = await _historyRepos.AddAsync(historyOrder, cancellationToken);
			if (result.IsError) { return result.Errors; }
			return result;
		}

		public async Task<ErrorOr<Success>> AddNewAudience(
            CreateAudienceRequest createAudienceRequest,
            CancellationToken cancellationToken)
		{
			Audience audience = new Audience();
			audience.Number = createAudienceRequest.number;
			audience.Orders = new List<Order>();
			audience.Capacity = createAudienceRequest.capacity;
			audience.Status = "WORKING";

			foreach (var category in createAudienceRequest.eventTypes)
			{
				audience.Type.Add(new AudienceType(category));
			}
			var result = await _audienceRepos.AddAsync(audience, cancellationToken);
			if (result.IsError) return result.Errors;
			return Result.Success;
		}

		private async Task<ErrorOr<bool>> _IsIntervalAvailable(string audienceNumber,
			DateTime startOfBooking, DateTime endOfBooking, CancellationToken cancellationToken)
		{
			var errorOrListBookedAudiences = await GetBookedAudiences(cancellationToken);

			if (errorOrListBookedAudiences.IsError)
				return errorOrListBookedAudiences.Errors;

			BookedAudiencesObject BookedAudiences =
				(BookedAudiencesObject)errorOrListBookedAudiences.Value.bookedAudiences;

			if (!BookedAudiences.ContainsKey(audienceNumber))
			{
				return false;
			}

			foreach (var interval in BookedAudiences[audienceNumber])
			{
				if (
					(startOfBooking >= interval.Item1 && startOfBooking <= interval.Item2)
					|| (endOfBooking >= interval.Item1 && endOfBooking <= interval.Item2)
					)
				{
					return false;

				}
			}
			return true;
		}

        public Task<ErrorOr<Success>> RemoveAudience(Guid audienceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<Success>> UpdateBookedAudiences(Guid audienceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
