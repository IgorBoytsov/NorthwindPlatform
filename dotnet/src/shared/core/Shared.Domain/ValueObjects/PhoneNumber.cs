using Common.Core.Guard;
using Common.Core.Results;
using Shared.Domain.Exception;
using Shared.Kernel.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace Shared.Domain.ValueObjects
{
    public readonly record struct PhoneNumber : IValueObject
    {
        /// <summary>
        /// Хранит номер телефона в каноническом формате E.164 (например, "+79991234567").
        /// </summary>
        public string Value { get; }

        private PhoneNumber(string value) => Value = value;

        /// <exception cref="PhoneNumberException">Если номер телефона не прошел валидацию.</exception>
        public static PhoneNumber Create(string phoneNumber)
        {
            Guard.Against.That(!TryCreate(phoneNumber, out var result, out var error), () => new PhoneNumberException(error!));

            return result!.Value;
        }

        /// <summary>
        /// Пытается создать экземпляр PhoneNumber из строки. Не бросает исключений.
        /// </summary>
        public static bool TryCreate(string phoneNumber, [NotNullWhen(true)] out PhoneNumber? result) => TryCreate(phoneNumber, out result, out _);

        /// <summary>
        /// Основная логика создания и валидации номера.
        /// </summary>
        private static bool TryCreate(string phoneNumber, [NotNullWhen(true)] out PhoneNumber? result, [NotNullWhen(false)] out Error? error)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                result = null;
                error = Error.Validation("Номер телефона не может быть пустым.");
                return false;
            }

            var sb = new StringBuilder();

            if (phoneNumber.TrimStart().StartsWith("+"))
                sb.Append('+');

            foreach (char c in phoneNumber)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
            }

            string sanitizedNumber = sb.ToString();

            if (!Regex.IsMatch(sanitizedNumber, @"^\+[1-9]\d{6,14}$"))
            {
                result = null;
                error = Error.Validation($"Неверный формат номера телефона: '{phoneNumber}'. Номер должен соответствовать международному формату.");
                return false;
            }

            result = new PhoneNumber(sanitizedNumber);
            error = null;
            return true;
        }

        /// <summary>
        /// Возвращает маскированную версию номера для безопасного отображения.
        /// Например, "+79991234567" -> "+7999***4567"
        /// </summary>
        /// <param name="visibleStartChars">Количество видимых символов в начале (после '+').</param>
        /// <param name="visibleEndChars">Количество видимых символов в конце.</param>
        /// <returns>Маскированная строка.</returns>
        public string ToMaskedString(int visibleStartChars = 4, int visibleEndChars = 4)
        {
            var numberPart = Value[1..];

            if (visibleStartChars + visibleEndChars >= numberPart.Length)
                return Value;

            var start = numberPart.Substring(0, visibleStartChars);
            var end = numberPart.Substring(numberPart.Length - visibleEndChars);
            var maskedPart = new string('*', numberPart.Length - visibleStartChars - visibleEndChars);

            return $"+{start}{maskedPart}{end}";
        }

        public override string ToString() => Value;

        public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    }
}