using Common.Core.Results;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Workstation.Infrastructure.Clients
{
    public interface IAuthClient
    {
        Task<Result<SrpChallengeResponse?>> GetSrpChallenge(SrpChallengeRequest request);
        Task<Result<AuthResponse?>> VerifierSrpProof(SrpVerifyRequest request);
    }
}