using Common.Core.Results;
using NorthwindPlatform.Staff.Service.Domain.Exceptions;

namespace NorthwindPlatform.Staff.Service.Domain.ValueObjects.Employees
{
    public readonly record struct Gender
    {
        private static readonly HashSet<string> ValidGenders = new(StringComparer.OrdinalIgnoreCase)
        {
            "Male",
            "Female"
        };

        public static readonly Gender Male = new("Male");
        public static readonly Gender Female = new("Female");

        public static readonly IReadOnlyList<Gender> AllowedValues = 
        [
            Male, 
            Female
        ];

        public readonly string Value { get; }

        private Gender(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !ValidGenders.Contains(value))
                throw new InvalidGenderException(Error.New(ErrorCode.Create,$"Gender '{value}' is not valid"));

            Value = value;    
        }

        public static Gender FromString(string value)
        {
            return new Gender(value);
        }

        public override string ToString() => Value;
        
        public static implicit operator string(Gender value) => value.ToString();
    }
}