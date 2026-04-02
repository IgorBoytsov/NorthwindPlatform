using Shared.Kernel.Primitives;

namespace NorthwindPlatform.Staff.Service.Application.Abstractions
{
    public interface IEventStoreRepository
    {
        Task SaveAsync<T, TId>(string streamId, T aggregate, long expectedVersion, CancellationToken ct = default) 
        where T : AggregateRoot<TId>
        where TId : notnull;
    }
}