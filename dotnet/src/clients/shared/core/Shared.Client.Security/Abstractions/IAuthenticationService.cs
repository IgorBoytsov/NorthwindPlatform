using Common.Core.Results;
using Shared.Contracts.Requests.Security;
using Shared.Contracts.Responses.Security;

namespace Shared.Client.Security.Abstractions
{
    public interface IAuthenticationService
    {
        Task<Result<AuthenticationResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
        Task<Result> LogoutAsync(CancellationToken cancellationToken = default);
        Task<Result<AuthenticationResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    }
}