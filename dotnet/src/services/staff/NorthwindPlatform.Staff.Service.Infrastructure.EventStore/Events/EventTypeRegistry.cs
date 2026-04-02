using NorthwindPlatform.Staff.Service.Domain.Events.Employees;

namespace NorthwindPlatform.Staff.Service.Infrastructure.EventStore.Events
{
    public class EventTypeRegistry
    {
        private readonly Dictionary<Type, string> _typeToNam = [];
        private readonly Dictionary<string, Type> _nameToType = [];

        public EventTypeRegistry()
        {
            Register<EmployeeCreated>(nameof(EmployeeCreated));
        }

        public void Register<T>(string eventName)
        {
            var type = typeof(T);
            _typeToNam[type] = eventName;
            _nameToType[eventName] = type;
        }

        public string GetEventName(Type type)
        {
            if (!_typeToNam.TryGetValue(type, out var name))
                throw new InvalidOperationException($"Event type {type.Name} not registered");

            return name;
        }

        public Type? GerEventType(string eventName)
        {
            _nameToType.TryGetValue(eventName, out var type);
            return type;
        }
    }
}