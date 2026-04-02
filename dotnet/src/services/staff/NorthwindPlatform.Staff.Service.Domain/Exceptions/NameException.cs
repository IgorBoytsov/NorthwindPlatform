using Common.Core.Results;
using Shared.Kernel.Exceptions;

namespace NorthwindPlatform.Staff.Service.Domain.Exceptions
{
    public sealed class NameException(Error error) : DomainException(error)
    {
        
    }
}