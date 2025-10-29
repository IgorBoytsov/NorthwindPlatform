using Common.Core.Results;

namespace Shared.Client.Http.Abstractions
{
    public interface IGetAllApiService<TResponse> where TResponse : class
    {
        Task<Result<List<TResponse>>> GetAllAsync();
    }
}