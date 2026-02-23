using Common.Core.Results;
using MediatR;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Workstation.Features.Auth.Command.VerifySrpProof
{
    public sealed record VerifySrpProofCommand(
        string Login,
        string A, 
        string M1,
        string DeviceId,
        byte[] DeviceFingerprintHash) : IRequest<Result<AuthResponse?>>;
}