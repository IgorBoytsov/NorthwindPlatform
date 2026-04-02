using Common.Core.Results;
using NorthwindPlatform.Staff.Service.Domain.Exceptions;

namespace NorthwindPlatform.Staff.Service.Domain.ValueObjects.Employees
{
    public readonly record struct Citizenship
    {        
        private static readonly HashSet<string> ValidCitizenship = new(StringComparer.OrdinalIgnoreCase)
        {
            "Российское",
            "Иностранное"
        };

        public static readonly Citizenship Russian = new("Российское");
        public static readonly Citizenship Foreign = new("Иностранное");

        public static readonly IReadOnlyList<Citizenship> AllowedValues = 
        [
            Russian,
            Foreign
        ];

        public readonly string Value { get; }

        private Citizenship(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !ValidCitizenship.Contains(value))
                throw new InvalidCitizenshipException(Error.New(ErrorCode.Create,$"Citizenship '{value}' is not valid"));

            Value = value;    
        }
        
        public static Citizenship FromString(string value)
        {
            return new Citizenship(value);
        }

        public override string ToString() => Value;
        
        public static implicit operator string(Citizenship value) => value.ToString();
    }
}