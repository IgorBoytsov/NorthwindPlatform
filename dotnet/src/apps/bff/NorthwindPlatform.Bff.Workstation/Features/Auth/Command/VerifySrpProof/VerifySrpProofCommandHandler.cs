using Common.Core.Results;
using MediatR;
using NorthwindPlatform.Bff.Workstation.Infrastructure.Clients;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Workstation.Features.Auth.Command.VerifySrpProof
{
    public sealed class VerifySrpProofCommandHandler(IAuthClient authClient) : IRequestHandler<VerifySrpProofCommand, Result<AuthResponse?>>
    {
        private readonly IAuthClient _authClient = authClient;

        public async Task<Result<AuthResponse?>> Handle(VerifySrpProofCommand request, CancellationToken cancellationToken)
        {
            var result = await _authClient.VerifierSrpProof(new SrpVerifyRequest(request.Login, request.A, request.M1));

            return result;
        }
    }
}