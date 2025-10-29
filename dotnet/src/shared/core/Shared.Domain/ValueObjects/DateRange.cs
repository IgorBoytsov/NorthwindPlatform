using Common.Core.Guard;
using Common.Core.Results;
using Shared.Domain.Exception;
using Shared.Kernel.Primitives;

namespace Shared.Domain.ValueObjects
{
    public readonly record struct DateRange : IValueObject
    {
        public DateOnly Start { get; }
        public DateOnly End { get; }

        private DateRange(DateOnly start, DateOnly end)
        {
            Start = start;
            End = end;
        }

        public static DateRange Create(DateOnly start, DateOnly end)
        {
            Guard.Against.That(start < end, () => new DateRangeException(Error.Validation("Дата старта не может быть позже дата окончания.")));

            return new DateRange(start, end);
        }

        /// <summary>
        /// Создания диапазона для одного дня.
        /// </summary>
        /// <param name="day">Конкретный день.</param>
        public static DateRange ForSingleDay(DateOnly day) => new(day, day);

        public int NumberOfDays => End.DayNumber - Start.DayNumber + 1;

        /// <summary>
        /// Проверяет, содержит ли диапазон указанную дату.
        /// </summary>
        public bool Contains(DateOnly date) => date >= Start && date <= End;

        /// <summary>
        /// Проверяет, пересекается ли текущий диапазон с другим.
        /// </summary>
        public bool Overlaps(DateRange other) => Start <= other.End && End >= other.Start;

        public override string ToString() => $"От {Start} до {End}";

        public static implicit operator string(DateRange dateRange) => dateRange.ToString();
    }
}