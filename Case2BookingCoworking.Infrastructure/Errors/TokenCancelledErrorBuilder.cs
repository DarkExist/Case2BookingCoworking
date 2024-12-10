namespace Case2BookingCoworking.Infrastructure.Errors
{
    public sealed class TokenCancelledErrorBuilder : BaseErrorBuilder
    {
        public TokenCancelledErrorBuilder()
        {
            _errorType = 499;
            _errorCode = "Client Closed Request";
            _errorDescription = "A Timeout Occured";
        }
    }
}
