using Common.Core.Guard;
using Common.Core.Results;
using Shared.Domain.Enums;
using Shared.Domain.Exception;
using Shared.Kernel.Primitives;

namespace Shared.Domain.ValueObjects
{
    public readonly record struct Dimensions : IValueObject, IEquatable<Dimensions>
    {
        public decimal Length { get; }
        public decimal Width { get; }
        public decimal Height { get; }
        public DimensionUnit Unit { get; }

        private Dimensions(decimal length, decimal width, decimal height, DimensionUnit unit)
        {
            Guard.Against.That(length < 0, () => new DimensionsException(Error.Validation("Длина не может быть негативным числом")));
            Guard.Against.That(width < 0, () => new DimensionsException(Error.Validation("Ширина не может быть негативным числом")));
            Guard.Against.That(height < 0, () => new DimensionsException(Error.Validation("Высота не может быть негативным числом")));

            Length = length;
            Width = width;
            Height = height;
            Unit = unit;
        }

        /// <exception cref="DimensionsException"></exception>
        public static Dimensions Create(decimal length, decimal width, decimal height, DimensionUnit dimensions)
            => new(length, width, height, dimensions);

        /// <exception cref="DimensionsException"></exception>
        public static Dimensions InMeters(decimal length, decimal width, decimal height)
            => new(length, width, height, DimensionUnit.Meter);

        /// <exception cref="DimensionsException"></exception>
        public static Dimensions InCentimeters(decimal length, decimal width, decimal height)
            => new(length, width, height, DimensionUnit.Centimeter);

        /// <exception cref="DimensionsException"></exception>
        public static Dimensions InMillimeters(decimal length, decimal width, decimal height)
            => new(length, width, height, DimensionUnit.Millimeter);

        /// <exception cref="DimensionsException"></exception>
        public static Dimensions InInches(decimal length, decimal width, decimal height)
            => new(length, width, height, DimensionUnit.Inch);

        /// <exception cref="DimensionsException"></exception>
        public static Dimensions InFoots(decimal length, decimal width, decimal height)
            => new(length, width, height, DimensionUnit.Foot);

        public decimal Volume => Length * Width * Height;

        public Dimensions ConvertTo(DimensionUnit targetUnit)
        {
            if (Unit == targetUnit)
                return this;

            decimal currentFactor = GetFactorToMillimeters(Unit);
            decimal targetFactor = GetFactorToMillimeters(targetUnit);

            decimal newLength = Length * currentFactor / targetFactor;
            decimal newWidth = Width * currentFactor / targetFactor;
            decimal newHeight = Height * currentFactor / targetFactor;

            return new Dimensions(newLength, newWidth, newHeight, targetUnit);
        }

        public bool Equals(Dimensions other)
        {
            if (Unit == other.Unit)
                return Length == other.Length && Width == other.Width && Height == other.Height;

            var otherConverted = other.ConvertTo(Unit);

            const decimal tolerance = 0.000001m;

            return Math.Abs(Length - otherConverted.Length) < tolerance &&
                   Math.Abs(Width - otherConverted.Width) < tolerance &&
                   Math.Abs(Height - otherConverted.Height) < tolerance;
        }

        public override int GetHashCode()
        {
            var baseDimensions = ConvertTo(DimensionUnit.Millimeter);
            return HashCode.Combine(baseDimensions.Length, baseDimensions.Width, baseDimensions.Height);
        }

        /// <summary>
        /// Получения коэффициента конвертации в миллиметры
        /// </summary>
        /// <exception cref="DimensionsException"></exception>
        private static decimal GetFactorToMillimeters(DimensionUnit unit) => unit switch
        {
            DimensionUnit.Millimeter => 1m,
            DimensionUnit.Centimeter => 10m,
            DimensionUnit.Meter => 1000m,
            DimensionUnit.Inch => 25.4m,
            DimensionUnit.Foot => 304.8m,
            _ => throw new DimensionsException(Error.Validation("Не известное значение."))
        };

        /// <summary>
        /// Красивого вывод : "mm", "cm" и тд
        /// </summary>
        private static string GetUnitAbbreviation(DimensionUnit unit) => unit switch
        {
            DimensionUnit.Millimeter => "mm",
            DimensionUnit.Centimeter => "cm",
            DimensionUnit.Meter => "m",
            DimensionUnit.Inch => "in",
            DimensionUnit.Foot => "ft",
            _ => ""
        };

        public override string ToString()
        {
            string unitAbbr = GetUnitAbbreviation(Unit);
            return $"{Length:0.##} x {Width:0.##} x {Height:0.##} {unitAbbr}";
        }
    }
}