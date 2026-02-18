using Common.Core.Results;
using MediatR;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Staff.Features.Auth.Command.SrpChallenge
{
    public sealed record GetSrpChallengeCommand(string Login) : IRequest<Result<SrpChallengeResponse?>>;
}