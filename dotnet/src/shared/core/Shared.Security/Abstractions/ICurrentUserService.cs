using System.Security.Claims;

namespace Shared.Security.Abstractions
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? UserEmail { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string roleName);
        IEnumerable<Claim> GetUserClaims();
    }
}