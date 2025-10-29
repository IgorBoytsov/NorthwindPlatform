using Common.Core.Guard;
using Common.Core.Results;
using Shared.Domain.Exception;
using Shared.Kernel.Primitives;

namespace Shared.Domain.ValueObjects
{
    public readonly record struct Weight : IValueObject, IComparable<Weight>
    {
        private const decimal PoundsToKilogramsFactor = 0.45359237m;
        private const decimal GramsToKilogramsFactor = 0.001m;

        public decimal Kilograms { get; }

        private Weight(decimal kilograms)
        {
            Guard.Against.That(kilograms < 0, () => new WeightException(Error.Validation("Вес не может быть отрицательным.")));

            Kilograms = kilograms;
        }

        public static readonly Weight Zero = new(0);

        /// <exception cref="WeightException">.</exception>
        public static Weight FromKilograms(decimal kilograms) => new(kilograms);

        /// <exception cref="WeightException">.</exception>
        public static Weight FromGrams(decimal grams) => new(grams * GramsToKilogramsFactor);

        /// <exception cref="WeightException">.</exception>
        public static Weight FromPounds(decimal pounds) => new(pounds * PoundsToKilogramsFactor);

        public decimal ToGrams() => Kilograms / GramsToKilogramsFactor;
        public decimal ToPounds() => Kilograms / PoundsToKilogramsFactor;

        // --- Перегрузка операторов ---

        /// <exception cref="WeightException">.</exception>
        public static Weight operator +(Weight left, Weight right) => new(left.Kilograms + right.Kilograms);

        /// <exception cref="WeightException">.</exception>
        public static Weight operator -(Weight left, Weight right) => new(left.Kilograms - right.Kilograms);

        /// <exception cref="WeightException">.</exception>
        public static Weight operator *(Weight weight, decimal multiplier)
        {
            if (multiplier < 0)
                throw new ArgumentException("Multiplier cannot be negative.", nameof(multiplier));
            return new Weight(weight.Kilograms * multiplier);
        }

        /// <exception cref="WeightException">.</exception>
        public static Weight operator /(Weight weight, decimal divisor)
        {
            Guard.Against.That(divisor <= 0, () => new WeightException(Error.Validation("Делитель должно быть позитивным числом.")));

            return new Weight(weight.Kilograms / divisor);
        }

        // --- IComparable ---

        public int CompareTo(Weight other) => Kilograms.CompareTo(other.Kilograms);

        /// <exception cref="WeightException">.</exception>
        public static bool operator <(Weight left, Weight right) => left.CompareTo(right) < 0;

        /// <exception cref="WeightException">.</exception>
        public static bool operator >(Weight left, Weight right) => left.CompareTo(right) > 0;

        /// <exception cref="WeightException">.</exception>
        public static bool operator <=(Weight left, Weight right) => left.CompareTo(right) <= 0;

        /// <exception cref="WeightException">.</exception>
        public static bool operator >=(Weight left, Weight right) => left.CompareTo(right) >= 0;

        public override string ToString()
        {
            if (Kilograms >= 1)
                return $"{Kilograms:F2} kg";

            return $"{ToGrams():F0} g";
        }

        public static implicit operator decimal(Weight weight) => weight.Kilograms;
    }
}