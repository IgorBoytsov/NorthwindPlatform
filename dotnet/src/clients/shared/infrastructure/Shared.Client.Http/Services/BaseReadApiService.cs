using Common.Core.Results;
using Shared.Client.Http.Abstractions;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shared.Client.Http.Services
{
    public abstract class BaseReadApiService<TResponse, TKey> : IBaseReadApiService<TResponse, TKey>
            where TResponse : class
    {
        public readonly HttpClient _httpClient;
        public readonly string _endpoint;
        protected readonly JsonSerializerOptions _jsonSerializerOptions;

        protected BaseReadApiService(HttpClient httpClient, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public virtual async Task<Result<List<TResponse>>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_endpoint);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<List<TResponse>>(_jsonSerializerOptions);
                return result ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine();
                return Error.New(ErrorCode.Get, $"Ошибка при извлечении всех элементов из {_endpoint}: {ex.Message}");
            }
        }

        public virtual async Task<Result<TResponse?>> GetByIdAsync(TKey id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_endpoint}/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TResponse>(_jsonSerializerOptions);
            }
            catch (HttpRequestException ex)
            {
                return Error.New(ErrorCode.Get, $"Ошибка при выборке элемента с идентификатором {id} из {_endpoint}: {ex.Message}");
            }
        }
    }
}