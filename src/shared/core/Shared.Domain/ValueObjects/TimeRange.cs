using Shared.Kernel.Primitives;

namespace Shared.Domain.ValueObjects
{
    public readonly record struct TimeRange : IValueObject
    {
        public TimeOnly Start { get; }
        public TimeOnly End { get; }

        private TimeRange(TimeOnly start, TimeOnly end)
        {
            Start = start;
            End = end;
        }

        public static TimeRange Create(TimeOnly start, TimeOnly end) => new TimeRange(start, end);

        /// <summary>
        /// Пересекает ли диапазон полночь.
        /// </summary>
        public bool SpansMidnight => End < Start;

        /// <summary>
        /// Возвращает продолжительность временного диапазона.
        /// Если пересекает полночь, например 22:00-02:00.
        /// Длительность = (время до полуночи) + (время после полуночи)
        /// (24:00 - 22:00) + (02:00 - 00:00) = 2ч + 2ч = 4ч
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                if (!SpansMidnight)
                    return End - Start;


                return TimeOnly.MaxValue - Start + TimeSpan.FromTicks(1) + (End - TimeOnly.MinValue);
            }
        }

        /// <summary>
        /// Проверяет, содержит ли диапазон указанное время.
        /// </summary>
        public bool Contains(TimeOnly time)
        {
            if (!SpansMidnight)
                return time >= Start && time < End;

            return time >= Start || time < End;
        }

        /// <summary>
        /// Проверяет, пересекается ли текущий диапазон с другим.
        /// </summary>
        public bool Overlaps(TimeRange other)
        {
            if (!SpansMidnight && !other.SpansMidnight)
                return Start < other.End && other.Start < End;

            if (SpansMidnight && other.SpansMidnight)
                return true;


            if (SpansMidnight) 
            {
                var firstPart = new TimeRange(Start, TimeOnly.MaxValue);
                var secondPart = new TimeRange(TimeOnly.MinValue, End);
                return other.Overlaps(firstPart) || other.Overlaps(secondPart);
            }
            else
            {
                return other.Overlaps(this);
            }
        }

        public override string ToString() => $"От {Start} до {End}";

        public static implicit operator string(TimeRange range) => range.ToString();
    }
}