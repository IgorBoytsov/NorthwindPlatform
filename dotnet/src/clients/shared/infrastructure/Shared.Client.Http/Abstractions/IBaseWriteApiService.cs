namespace Shared.Client.Http.Abstractions
{
    public interface IBaseWriteApiService<TResponse, in TKey> :
        IAddApiService<TResponse>,
        IUpdateApiService<TKey>,
        IDeleteApiService<TKey>
        where TResponse : class
    {
    }
}