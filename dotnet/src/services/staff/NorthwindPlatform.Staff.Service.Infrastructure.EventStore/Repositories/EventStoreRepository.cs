using KurrentDB.Client;
using NorthwindPlatform.Staff.Service.Application.Abstractions.Repositories;
using NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Events;
using Shared.Kernel.Primitives;

namespace NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Repositories
{
    public class EventStoreRepository(KurrentDBClient client, IEventMapper eventMapper) : IEventStoreRepository
    {
        private readonly KurrentDBClient _client = client;
        private readonly IEventMapper _eventMapper = eventMapper;

        public async Task SaveAsync<T, TId>(string streamId, T aggregate, long expectedVersion, CancellationToken ct = default) 
        where T : AggregateRoot<TId>
        where TId : notnull
        {
            var events = aggregate.GetDomainEvents();

            if (events.Count == 0)
                return;

            var eventData = events.Select(_eventMapper.MapToEventData);

            if (expectedVersion == -1)
            {
                await _client.AppendToStreamAsync(streamId, StreamState.NoStream, eventData, cancellationToken: ct);
            }
            else if (expectedVersion == -2)
            {
                await _client.AppendToStreamAsync(streamId, StreamState.Any, eventData, cancellationToken: ct);
            }
            else
            {
                var expectedRevision  = (ulong)expectedVersion;
                await _client.AppendToStreamAsync(streamId, expectedRevision, eventData, cancellationToken: ct);
            }

            aggregate.ClearDomainEvents();
        }
    }
}