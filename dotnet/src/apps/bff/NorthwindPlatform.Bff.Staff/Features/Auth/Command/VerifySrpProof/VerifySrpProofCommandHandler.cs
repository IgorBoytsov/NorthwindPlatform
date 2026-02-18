using Common.Core.Results;
using MediatR;
using NorthwindPlatform.Bff.Staff.Infrastructure.Clients;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Staff.Features.Auth.Command.VerifySrpProof
{
    public sealed class VerifySrpProofCommandHandler(IAuthClient authClient) : IRequestHandler<VerifySrpProofCommand, Result<AuthResponse?>>
    {   
        private readonly IAuthClient _authClient = authClient;

        public async Task<Result<AuthResponse?>> Handle(VerifySrpProofCommand request, CancellationToken cancellationToken)
        {
            var authResult = await _authClient.VerifierSrpProof(new SrpVerifyRequest(request.Login, request.A, request.M1));

            if (authResult.IsFailure)
                return Result<AuthResponse?>.Failure(authResult.Errors);

            return authResult;
        }
    }
}