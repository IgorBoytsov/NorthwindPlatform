using System.IdentityModel.Tokens.Jwt;
using Common.Core.Results;
using MediatR;
using NorthwindPlatform.Bff.Workstation.Infrastructure.Clients;
using NorthwindPlatform.Bff.Workstation.Infrastructure.Repositories;
using NorthwindPlatform.Bff.Workstation.Models.Domain.Entities;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Workstation.Features.Auth.Command.VerifySrpProof
{
    public sealed class VerifySrpProofCommandHandler(
        IAuthClient authClient,
        ITrustedDeviceRepository trustedDeviceRepository) : IRequestHandler<VerifySrpProofCommand, Result<AuthResponse?>>
    {   
        private readonly IAuthClient _authClient = authClient;
        private readonly ITrustedDeviceRepository _trustedDeviceRepository = trustedDeviceRepository;

        public async Task<Result<AuthResponse?>> Handle(VerifySrpProofCommand request, CancellationToken cancellationToken)
        {
            var authResult = await _authClient.VerifierSrpProof(new SrpVerifyRequest(request.Login, request.A, request.M1));

            if (authResult.IsFailure)
                return Result<AuthResponse?>.Failure(authResult.Errors);

            var tokens = authResult.Value;
            var existingDevice = await _trustedDeviceRepository.GetByDeviceIdAsync(request.DeviceId);
            var (userId, sessionId) = ExtractClaimsFromAccessToken(tokens!.AccessToken);

            if (existingDevice == null)
            {
                var newDevice = new TrustedDevice
                {
                    Id = Guid.NewGuid(),  
                    UserId = userId,
                    DeviceId = request.DeviceId,
                    DeviceFingerprintHash = request.DeviceFingerprintHash,  
                    AuthServiceSessionId = sessionId,      
                    FirstSeenAt = DateTime.UtcNow,
                    LastActivityAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(30),
                    IsActive = true,
                    CreatedIp = null,
                    LastIp = null          
                };

                await _trustedDeviceRepository.InsertAsync(newDevice);
            }
            else
            {
                await _trustedDeviceRepository.UpdateLastActivityAsync(request.DeviceId, DateTime.UtcNow, null);    
            }

            return authResult;
        }

        private (Guid UserId, string SessionId) ExtractClaimsFromAccessToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);

            var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            var jtiClaim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

            if (userIdClaim?.Value == null || jtiClaim?.Value == null)
                throw new InvalidOperationException("У Access токена отсутствуют необходимые значения.");

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
                throw new InvalidOperationException("Неверный идентификатор пользователя в токене.");

            return (userId, jtiClaim.Value);
        }
    }
}