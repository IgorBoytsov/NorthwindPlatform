using Common.Core.Results;
using Shared.Kernel.Exceptions;

namespace Shared.Domain.Exception
{
    public sealed class WeightException(Error error) : DomainException(error);
}