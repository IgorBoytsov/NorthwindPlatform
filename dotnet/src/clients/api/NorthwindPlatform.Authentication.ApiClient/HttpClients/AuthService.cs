using Common.Core.Results;
using Shared.Client.Security.Abstractions;
using Shared.Contracts.Enums;
using Shared.Contracts.Requests.Security;
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

        public async Task<Result<AuthenticationResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(ApiClientName.BaseAuthApi.ToString());

                var response = await client.PostAsJsonAsync("api/auth/login", request, _jsonSerializerOptions, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                    return Error.New(ErrorCode.ApiError, $"Сервер вернул ошибку: {response.StatusCode}. Детали: {errorContent}");
                }

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