namespace Common.Core.Extensions
{
    public static class IntExtensions
    {
        #region Преобразования в TimeSpan

        public static TimeSpan Days(this int value) => TimeSpan.FromDays(value);
        public static TimeSpan Hours(this int value) => TimeSpan.FromHours(value);
        public static TimeSpan Minutes(this int value) => TimeSpan.FromMinutes(value);
        public static TimeSpan Seconds(this int value) => TimeSpan.FromSeconds(value);
        public static TimeSpan Milliseconds(this int value) => TimeSpan.FromMilliseconds(value);

        #endregion

        #region Проверки диапазона и четности

        /// <summary>
        /// Проверяет, находится ли число в указанном диапазоне (включительно).
        /// </summary>
        /// <param name="value">Проверяемое число.</param>
        /// <param name="min">Минимальное значение диапазона.</param>
        /// <param name="max">Максимальное значение диапазона.</param>
        /// <returns>True, если число находится в диапазоне.</returns>
        /// <example>if (age.IsBetween(18, 65)) { ... }</example>
        public static bool IsBetween(this int value, int min, int max) => value >= min && value <= max;

        /// <summary>
        /// Проверяет, является ли число четным.
        /// </summary>
        /// <returns>True, если число четное.</returns>
        public static bool IsEven(this int value) => value % 2 == 0;

        /// <summary>
        /// Проверяет, является ли число нечетным.
        /// </summary>
        /// <returns>True, если число нечетное.</returns>
        public static bool IsOdd(this int value) => value % 2 != 0;

        #endregion

        #region Форматирование

        /// <summary>
        /// Преобразует число в строку с порядковым суффиксом (1st, 2nd, 3rd, 4th).
        /// </summary>
        /// <param name="value">Число для преобразования.</param>
        /// <returns>Строка с порядковым суффиксом.</returns>
        /// <example>string place = 3.ToOrdinal(); // "3rd"</example>
        public static string ToOrdinal(this int value)
        {
            if (value % 100 >= 11 && value % 100 <= 13)
                return $"{value}th";

            return (value % 10) switch
            {
                1 => $"{value}st",
                2 => $"{value}nd",
                3 => $"{value}rd",
                _ => $"{value}th",
            };
        }

        #endregion
    }
}