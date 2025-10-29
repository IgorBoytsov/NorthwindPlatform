using Common.Core.Results;

namespace Shared.Client.Http.Abstractions
{
    public interface IAddApiService<TResponse> where TResponse : class
    {
        Task<Result<TResponse?>> AddAsync<TRequest>(TRequest newItem);
        Task<Result> CreateAsync<TRequest>(TRequest newItem);
    }
}