using Common.Core.Results;
using MediatR;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Passenger.Features.Auth.Command.VerifySrpProof
{
    public sealed record VerifySrpProofCommand(
        string Login,
        string A, 
        string M1) : IRequest<Result<AuthResponse?>>;
}