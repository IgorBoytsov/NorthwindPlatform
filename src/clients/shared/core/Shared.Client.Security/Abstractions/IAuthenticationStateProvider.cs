using Shared.Contracts.Responses.Security;

namespace Shared.Client.Security.Abstractions
{
    public interface IAuthenticationStateProvider
    {
        Task<bool> IsAuthenticatedAsync();
        Task<UserResponse> GetCurrentUserAsync();
    }
}