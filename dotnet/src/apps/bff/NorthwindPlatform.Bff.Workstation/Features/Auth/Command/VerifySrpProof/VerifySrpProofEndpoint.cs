using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Requests.Workstation;

namespace NorthwindPlatform.Bff.Workstation.Features.Auth.Command.VerifySrpProof
{
    public static class VerifySrpProofEndpoint
    {
        public static void MapVerifySrpProof(this IEndpointRouteBuilder app)
        {
            app.MapPost("srp/verify", async ([FromBody] WorkstationSrpVerifyRequest request, [FromServices] IMediator mediator, CancellationToken ct) =>
            {
                var result = await mediator.Send(new VerifySrpProofCommand(request.Login, request.A, request.M1, request.DeviceId, Convert.FromBase64String(request.DeviceFingerprintHash)), ct);

                if (result.IsFailure)
                    return Results.BadRequest(result.Errors);

                return Results.Ok(result.Value);
            });
        }
    }
}