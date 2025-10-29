using Common.Core.Guard;
using Common.Core.Results;
using Shared.Domain.Exception;
using Shared.Kernel.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace Shared.Domain.ValueObjects
{
    public readonly record struct Email : IValueObject
    {
        public string Value { get; }
         
        private Email(string value) => Value = value;

        /// <exception cref="EmailAddressException"></exception>
        public static Email Create(string email)
        {
            Guard.Against.That(string.IsNullOrWhiteSpace(email), () => new EmailAddressException(Error.Validation("Email не может быть пустым!")));
            Guard.Against.That(!MailAddress.TryCreate(email, out _), () => new EmailAddressException(Error.Validation($"Неверный формат Email: '{email}'")));

            return new Email(email);
        }

        public static bool TryCreate(string email, [NotNullWhen(true)] out Email? result)
        {
            if (string.IsNullOrWhiteSpace(email) || !MailAddress.TryCreate(email, out _))
            {
                result = null;
                return false;
            }

            result = new Email(email);
            return true;
        }

        public override string ToString() => Value;

        public static implicit operator string(Email email) => email.Value;
    }
}