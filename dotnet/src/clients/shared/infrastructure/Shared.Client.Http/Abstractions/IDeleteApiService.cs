using Common.Core.Results;

namespace Shared.Client.Http.Abstractions
{
    public interface IDeleteApiService<in TKey>
    {
        Task<Result> DeleteAsync(TKey id);
    }
}