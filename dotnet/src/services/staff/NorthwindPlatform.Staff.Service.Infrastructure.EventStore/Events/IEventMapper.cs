using KurrentDB.Client;

namespace NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Events
{
    public interface IEventMapper
    {
        EventData MapToEventData(object domainEvent);
        object? MapToDomainEvent(ResolvedEvent resolvedEvent);
    }
}