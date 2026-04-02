using Common.Core.Results;
using NorthwindPlatform.Staff.Service.Domain.Exceptions;

namespace NorthwindPlatform.Staff.Service.Domain.ValueObjects.Employees
{
    public readonly record struct Surname
    {
        public const int MaxLength = 50;
        public const int MinLength = 2;

        public readonly string Value { get; }

        private Surname(string value)
        {
            Value = value;
        }

        public static Surname Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NameException(Error.New(ErrorCode.Validation, "Surname cannot be empty"));

            if (value.Length > 50 || value.Length < 2)
                throw new NameException(Error.New(ErrorCode.Validation, $"Surname Length must be from {MinLength} to {MaxLength} symbols"));

            return new Surname(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator string(Surname value) => value.ToString();  
    }
}