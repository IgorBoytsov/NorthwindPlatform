using Common.Core.Results;

namespace Shared.Client.Http.Abstractions
{
    public interface IUpdateApiService<in TKey>
    {
        Task<Result> UpdateAsync<TRequest>(TKey id, TRequest updatedItem);
    }
}