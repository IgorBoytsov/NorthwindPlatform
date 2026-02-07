using Common.Core.Results;
using Shared.Client.Security.Abstractions;
using Shared.Contracts.Enums;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Requests.Security;
using Shared.Contracts.Responses.AuthenticationService;
using Shared.Contracts.Responses.Security;
using System.Net.Http.Json;
using System.Text.Json;

namespace NorthwindPlatform.Authentication.ApiClient.HttpClients
{
    public sealed class AuthService(IHttpClientFactory httpClientFactory) : IAuthenticationService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<Result<SrpChallengeResponse>> GetSrpChallenge(SrpChallengeRequest request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(ApiClientName.BaseAuthApi.ToString());
                var response = await client.PostAsJsonAsync("srp/challenge", request, _jsonSerializerOptions);
                response.EnsureSuccessStatusCode();

                var resultData = await response.Content.ReadFromJsonAsync<SrpChallengeResponse>();
                
                return resultData!;
            }
            catch (HttpRequestException ex)
            {
                return Error.New(ErrorCode.ApiError, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.New(ErrorCode.ApiError, $"Произошла критическая ошибки при отправки запроса: {ex.Message}");
            }
        }

        public async Task<Result<AuthResponse>> VerifySrpProof(SrpVerifyRequest request)
        {
            HttpResponseMessage? response = null!;
            try
            {
                var client = _httpClientFactory.CreateClient(ApiClientName.BaseAuthApi.ToString());
                response = await client.PostAsJsonAsync("srp/verify", request, _jsonSerializerOptions);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return Error.New(ErrorCode.ApiError, 
                        $"HTTP {response.StatusCode}: {errorContent}");
                }

                var resultData = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (resultData is null)
                {
                    return Error.New(ErrorCode.ApiError, "Пустой или некорректный JSON-ответ от сервера");
                }

                return resultData!;
            }
                catch (JsonException ex) when (ex.Message.Contains("could not be converted"))
            {
                var rawContent = await response.Content.ReadAsStringAsync(); 
                return Error.New(ErrorCode.ApiError, $"Ошибка десериализации AuthResponse. Ответ сервера: {rawContent}\nОшибка: {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                return Error.New(ErrorCode.ApiError, ex.Message);
            }
            catch (Exception ex)
             {
                return Error.New(ErrorCode.ApiError, $"Произошла критическая ошибка при отправке запроса: {ex.Message}");
             }

        }

        public async Task<Result<AuthenticationResponse>> LoginByTokenAsync(LoginByTokenRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(ApiClientName.BaseAuthApi.ToString());

                var response = await client.PostAsJsonAsync("api/auth/token-login", request, _jsonSerializerOptions, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadFromJsonAsync<AuthenticationResponse>(cancellationToken);

                return responseData!;
            }
            catch (HttpRequestException ex)
            {
                return Error.New(ErrorCode.ApiError, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.New(ErrorCode.ApiError, $"Произошла критическая ошибки при отправки запроса: {ex.Message}");
            }
        }

        public Task<Result> LogoutAsync(CancellationToken cancellationToken = default)
        {
            return null!;
        }

        public async Task<Result<AuthenticationResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(ApiClientName.BaseAuthApi.ToString());
                var response = await client.PostAsJsonAsync("api/auth/token-login", request, _jsonSerializerOptions, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadFromJsonAsync<AuthenticationResponse>(cancellationToken);

                return responseData!;
            }
            catch (HttpRequestException ex)
            {
                return Error.New(ErrorCode.ApiError, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.New(ErrorCode.ApiError, $"Произошла критическая ошибки при отправки запроса: {ex.Message}");
            }
        }
    }
}