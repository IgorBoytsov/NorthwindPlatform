namespace Common.Core.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Проверяет, является ли последовательность null или не содержит элементов.
        /// Упрощает общую проверку if (collection == null || !collection.Any()).
        /// </summary>
        /// <typeparam name="T">Тип элементов в последовательности.</typeparam>
        /// <param name="source">Проверяемая последовательность.</param>
        /// <returns>True, если последовательность null или пуста, иначе false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source) => source == null || !source.Any();

        /// <summary>
        /// Преобразует последовательность в строку с указанным разделителем.
        /// </summary>
        /// <typeparam name="T">Тип элементов в последовательности.</typeparam>
        /// <param name="source">Последовательность для объединения.</param>
        /// <param name="separator">Строка-разделитель. По умолчанию: ", ".</param>
        /// <returns>Строка, состоящая из элементов последовательности, разделенных указанным разделителем.</returns>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, string separator = ", ")
        {
            if (source is null)
                return string.Empty;

            return string.Join(separator, source);
        }
    }
}