using Shared.Contracts.Responses.Security;
using System.Security.Claims;

namespace Shared.Client.Security.Abstractions
{
    public interface IUserContext
    {
        bool IsAuthenticated { get; }
        Guid UserId { get; }
        string? Email { get; }
        string? UserName { get; }
        string? FirstName { get; }
        string? LastName { get; }

        IReadOnlyCollection<string> Roles { get; }
        IReadOnlyCollection<Claim> Claims { get; }

        bool IsInRole(string roleName);
        string? GetClaimValue(string claimType);

        void SetUser(AuthenticationResponse authResponse);
        void ClearUser();
    }
}