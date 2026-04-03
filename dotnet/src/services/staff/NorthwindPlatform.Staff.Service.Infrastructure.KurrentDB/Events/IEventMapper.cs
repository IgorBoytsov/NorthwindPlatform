using KurrentDB.Client;

namespace NorthwindPlatform.Staff.Service.Infrastructure.KurrentDB.Events
{
    public interface IEventMapper
    {
        EventData MapToEventData(object domainEvent);
        object? MapToDomainEvent(ResolvedEvent resolvedEvent);
    }
}