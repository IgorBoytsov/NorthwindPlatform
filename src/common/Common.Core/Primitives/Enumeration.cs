using System.Reflection;

namespace Common.Core.Primitives
{
    public abstract record Enumeration<TEnum, TValue> : IEquatable<Enumeration<TEnum, TValue>>
        where TEnum : Enumeration<TEnum, TValue>
        where TValue : notnull, IEquatable<TValue>
    {
        private static readonly Lazy<Dictionary<TValue, TEnum>> Enumerations = new(CreateEnumerations);

        protected Enumeration(TValue id, string name)
        {
            Id = id;
            Name = name;
        }

        public TValue Id { get; }
        public string Name { get; }

        public static Maybe<TEnum> FromValue(TValue id) => Enumerations.Value.TryGetValue(id, out TEnum? enumeration) ? Maybe<TEnum>.Some(enumeration) : Maybe<TEnum>.None;

        public static Maybe<TEnum> FromName(string name)
        {
            TEnum? foundEnumeration = Enumerations.Value.Values
                .SingleOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return Maybe<TEnum>.Some(foundEnumeration!);
        }

        public static IReadOnlyCollection<TEnum> GetAll() => Enumerations.Value.Values;

        /// <summary>
        /// Определяет, равен ли текущий объект другому объекту того же типа. Сравнение идет только по Id.
        /// </summary>
        public virtual bool Equals(Enumeration<TEnum, TValue>? other)
        {
            if (other is null)
                return false;

            return GetType() == other.GetType() && Id.Equals(other.Id);
        }

        /// <summary>
        /// Возвращает хэш-код для текущего объекта. Основан только на Id.
        /// </summary>
        public override int GetHashCode() => Id.GetHashCode();

        /// <summary>
        /// Возвращает строковое представление объекта, которым является его имя.
        /// </summary>
        public override string ToString() => Name;

        private static Dictionary<TValue, TEnum> CreateEnumerations()
        {
            var enumerationType = typeof(TEnum);

            var fields = enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
                .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

            return fields.ToDictionary(x => x.Id);
        }
    }
}