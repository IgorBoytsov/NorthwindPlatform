namespace Shared.Client.Http.Abstractions
{
    public interface IBaseReadApiService<TResponse, in TKey> :
        IGetAllApiService<TResponse>,
        IGetByIdApiService<TResponse, TKey>
        where TResponse : class
    {
    }
}