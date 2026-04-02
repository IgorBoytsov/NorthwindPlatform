using Common.Core.Results;
using NorthwindPlatform.Staff.Service.Domain.Exceptions;

namespace NorthwindPlatform.Staff.Service.Domain.ValueObjects.Employees
{
    public readonly record struct Patronymic
    {
        public const int MaxLength = 50;
        public const int MinLength = 2;

        public readonly string Value { get; }

        private Patronymic(string value)
        {
            Value = value;
        }

        public static Patronymic Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NameException(Error.New(ErrorCode.Validation, "Patronymic cannot be empty"));

            if (value.Length > 50 || value.Length < 2)
                throw new NameException(Error.New(ErrorCode.Validation, $"Patronymic Length must be from {MinLength} to {MaxLength} symbols"));

            return new Patronymic(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator string(Patronymic value) => value.ToString();  
    }
}