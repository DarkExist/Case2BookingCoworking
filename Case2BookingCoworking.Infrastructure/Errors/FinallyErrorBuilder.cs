using ErrorOr;

namespace Case2BookingCoworking.Infrastructure.Errors
{
    public sealed class FinallyErrorBuilder : BaseErrorBuilder
    {
        public override Error BuildError()
        {
            return Error.Failure();
        }
    }
}
