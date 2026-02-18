using Common.Core.Results;
using MediatR;
using NorthwindPlatform.Bff.Staff.Infrastructure.Clients;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Staff.Features.Auth.Command.SrpChallenge
{
    public sealed class GetSrpChallengeCommandHandler(IAuthClient authClient) : IRequestHandler<GetSrpChallengeCommand, Result<SrpChallengeResponse?>>
    {
        private readonly IAuthClient _authClient = authClient;
        
        public async Task<Result<SrpChallengeResponse?>> Handle(GetSrpChallengeCommand request, CancellationToken cancellationToken)
        {
            var result = await _authClient.GetSrpChallenge(new SrpChallengeRequest(request.Login));

            return result;
        }
    }
}