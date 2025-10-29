using Common.Core.Results;
using Shared.Kernel.Exceptions;

namespace Shared.Domain.Exception
{
    public sealed class DimensionsException(Error error) : DomainException(error);
}