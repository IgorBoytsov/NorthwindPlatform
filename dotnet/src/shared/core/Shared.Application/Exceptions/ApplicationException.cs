using Common.Core.Results;

namespace Shared.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public Error Error { get; } = null!;

        public ApplicationException(Error error) : base(error.Message) => this.Error = error;

        public ApplicationException(Error error, Exception? innerException) : base(error.Message, innerException) => this.Error = error;
    }

}