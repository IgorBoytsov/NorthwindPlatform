using Common.Core.Guard;
using Common.Core.Results;
using Shared.Domain.Enums;
using Shared.Domain.Exception;
using Shared.Kernel.Primitives;

namespace Shared.Domain.ValueObjects
{
    public readonly record struct Money : IValueObject, IComparable<Money>
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        /// <exception cref="MoneyException"></exception>
        private Money(decimal amount, Currency currency)
        {
            Guard.Against.That(currency == Currency.None, () => new MoneyException(Error.Validation("Необходимо указать валюту")));

            Amount = amount;
            Currency = currency;
        }

        #region Фабричные методы

        /// <summary>
        /// Создает новый объект Money в рублях.
        /// </summary>
        public static Money Rubles(decimal amount) => new(amount, Currency.RUB);

        /// <summary>
        /// Создает новый объект Money в американских долларах.
        /// </summary>
        public static Money Dollars(decimal amount) => new(amount, Currency.USD);

        /// <summary>
        /// Создает новый объект Money в евро.
        /// </summary>
        public static Money Euros(decimal amount) => new(amount, Currency.EUR);

        #endregion

        #region Перегрузка операторов

        /// <exception cref="MoneyException"></exception>
        public static Money operator +(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return new Money(a.Amount + b.Amount, a.Currency);
        }

        /// <exception cref="MoneyException"></exception>
        public static Money operator -(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return new Money(a.Amount - b.Amount, a.Currency);
        }

        public static Money operator *(Money money, decimal multiplier)
            => new(money.Amount * multiplier, money.Currency);

        /// <exception cref="MoneyException"></exception>
        public static Money operator /(Money money, decimal divisor)
        {
            Guard.Against.That(divisor == 0, () => new DivideByZeroException("Нельзя делить сумму на ноль"));

            return new Money(money.Amount / divisor, money.Currency);
        }

        /// <exception cref="MoneyException"></exception>
        public static bool operator >(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return a.Amount > b.Amount;
        }

        /// <exception cref="MoneyException"></exception>
        public static bool operator <(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return a.Amount < b.Amount;
        }

        /// <exception cref="MoneyException"></exception>
        public static bool operator >=(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return a.Amount >= b.Amount;
        }

        /// <exception cref="MoneyException"></exception>
        public static bool operator <=(Money a, Money b)
        {
            EnsureSameCurrency(a, b);
            return a.Amount <= b.Amount;
        }

        #endregion

        /// <summary>
        /// Сравнивает этот экземпляр с указанным денежным объектом.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Целое число, указывающее относительный порядок сравниваемых объектов</returns>
        /// <exception cref="MoneyException"></exception>
        public int CompareTo(Money other)
        {
            EnsureSameCurrency(this, other);
            return Amount.CompareTo(other.Amount);
        }

        /// <summary>
        /// Возвращает строковое представление объекта money.
        /// </summary>
        public override string ToString() => $"{Amount:F2} {Currency}";

        private static void EnsureSameCurrency(Money a, Money b)
            => Guard.Against.That(a.Currency != b.Currency, () => new MoneyException(Error.Validation("Невозможно выполнить операцию с деньгами в разных валютах.")));
    }
}