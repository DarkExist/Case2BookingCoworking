using ErrorOr;

namespace Case2BookingCoworking.Infrastructure.Errors
{
    public abstract class BaseErrorBuilder
    {
        protected int _errorType;
        protected string _errorCode = "";
        protected string _errorDescription = "";

        public BaseErrorBuilder()
        {

        }

        public BaseErrorBuilder SetErrorType(int errorType)
        {
            _errorType = errorType;
            return this;
        }

        public BaseErrorBuilder SetErrorCode(string errorCode)
        {
            _errorCode = errorCode;
            return this;
        }

        public BaseErrorBuilder SetErrorDescription(string errorDescription)
        {
            _errorDescription = errorDescription;
            return this;
        }

        public virtual Error BuildError()
        {
            return Error.Custom(type: _errorType, code: _errorCode, description: _errorDescription);
        }
    }
}
