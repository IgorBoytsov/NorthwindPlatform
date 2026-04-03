using System.Text.Json;
using System.Text.Json.Serialization;
using KurrentDB.Client;

namespace NorthwindPlatform.Staff.Service.Infrastructure.KurrentDB.Events
{
    public class EventMapper(EventTypeRegistry registry) : IEventMapper
    {
        private readonly EventTypeRegistry _registry = registry;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          WriteIndented = false,
          Converters = { new JsonStringEnumConverter() }  
        };

        public object? MapToDomainEvent(ResolvedEvent resolvedEvent)
        {
            var eventName = resolvedEvent.Event.EventType;
            var eventType = _registry.GerEventType(eventName) ?? throw new InvalidOperationException($"Unknown event type: {eventName}");

            var data = resolvedEvent.Event.Data.ToArray();

            return JsonSerializer.Deserialize(data, eventType, _jsonOptions);
        }

        public EventData MapToEventData(object domainEvent)
        {
            var eventType = domainEvent.GetType();
            var eventName = _registry.GetEventName(eventType);
            var eventData = JsonSerializer.SerializeToUtf8Bytes(domainEvent, _jsonOptions);

            return new EventData(Uuid.NewUuid(), eventName, data: eventData);
        }
    }
}