using Common.Core.Results;
using Shared.Kernel.Exceptions;

namespace NorthwindPlatform.Staff.Service.Domain.Exceptions
{
    public class InvalidGenderException(Error error) : DomainException(error)
    {
        
    }
}