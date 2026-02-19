using System.Text.Json;
using Common.Core.Results;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Responses.AuthenticationService;

namespace NorthwindPlatform.Bff.Passenger.Infrastructure.Clients
{
    public sealed class AuthClient(HttpClient client) : IAuthClient
    {
        private readonly HttpClient _httpClient = client;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<Result<SrpChallengeResponse?>> GetSrpChallenge(SrpChallengeRequest request)
        {
            try
            {            
                var response = await _httpClient.PostAsJsonAsync("api/auth/srp/challenge", request, _jsonSerializerOptions);
                response.EnsureSuccessStatusCode();

                return Result<SrpChallengeResponse?>.Success(await response.Content.ReadFromJsonAsync<SrpChallengeResponse>());
            }
            catch (Exception ex)
            {
                return Result<SrpChallengeResponse?>.Failure(Error.New(ErrorCode.ApiError, $"Произошла ошибка при получение Srp Челленджа: {ex}"));
            }
        }

        public async Task<Result<AuthResponse?>> VerifierSrpProof(SrpVerifyRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/srp/verify", request, _jsonSerializerOptions);
                response.EnsureSuccessStatusCode();

                return Result<AuthResponse?>.Success(await response.Content.ReadFromJsonAsync<AuthResponse>());
            }
            catch (Exception)
            {
                return Result<AuthResponse?>.Failure(Error.New(ErrorCode.ApiError, "Произошла ошибка при верификации"));
            }
        }
    }
}