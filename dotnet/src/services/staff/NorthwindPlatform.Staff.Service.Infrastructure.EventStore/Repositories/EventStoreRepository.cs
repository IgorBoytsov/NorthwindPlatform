using EventStore.Client;
using NorthwindPlatform.Staff.Service.Application.Abstractions;
using NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Events;
using Shared.Kernel.Primitives;

namespace NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Repositories
{
    public class EventStoreRepository(EventStoreClient client, IEventMapper eventMapper) : IEventStoreRepository
    {
        private readonly EventStoreClient _client = client;
        private readonly IEventMapper _eventMapper = eventMapper;

        public async Task SaveAsync<T, TId>(string streamId, T aggregate, long expectedVersion, CancellationToken ct = default) 
        where T : AggregateRoot<TId>
        where TId : notnull
        {
            var events = aggregate.GetDomainEvents();

            if (events.Count == 0)
                return;

            var eventData = events.Select(_eventMapper.MapToEventData);

            var streamRevision = expectedVersion == -1 ? StreamRevision.None : new StreamRevision((ulong)expectedVersion);

            await _client.AppendToStreamAsync(streamId, streamRevision, eventData, cancellationToken: ct);

            aggregate.ClearDomainEvents();
        }
    }
}