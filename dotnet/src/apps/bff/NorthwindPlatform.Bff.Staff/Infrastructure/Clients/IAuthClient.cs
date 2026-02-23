using Common.Core.Results;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Staff.Infrastructure.Clients
{
    public interface IAuthClient
    {
        Task<Result<SrpChallengeResponse?>> GetSrpChallenge(SrpChallengeRequest request);
        Task<Result<AuthResponse?>> VerifierSrpProof(SrpVerifyRequest request);
    }
}