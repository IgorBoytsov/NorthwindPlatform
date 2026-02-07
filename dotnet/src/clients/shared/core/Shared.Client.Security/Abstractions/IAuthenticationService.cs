using Common.Core.Results;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Requests.Security;
using Shared.Contracts.Responses.AuthenticationService;
using Shared.Contracts.Responses.Security;

namespace Shared.Client.Security.Abstractions
{
    public interface IAuthenticationService
    {
        Task<Result<AuthResponse>> VerifySrpProof(SrpVerifyRequest request);
        Task<Result<SrpChallengeResponse>> GetSrpChallenge(SrpChallengeRequest request);
        Task<Result<AuthenticationResponse>> LoginByTokenAsync(LoginByTokenRequest request, CancellationToken cancellationToken = default);
        Task<Result> LogoutAsync(CancellationToken cancellationToken = default);
        Task<Result<AuthenticationResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    }
}