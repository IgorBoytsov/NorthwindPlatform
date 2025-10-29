using Common.Core.Results;

namespace Shared.Client.Http.Abstractions
{
    public interface IGetByIdApiService<TResponse, in TKey> where TResponse : class
    {
        Task<Result<TResponse?>> GetByIdAsync(TKey id);
    }
}