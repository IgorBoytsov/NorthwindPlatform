using System.Text;
using System.Text.RegularExpressions;

namespace Common.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Проверяет, не является ли строка null, пустой или состоящей только из пробельных символов.
        /// Является более читаемой альтернативой !string.IsNullOrWhiteSpace(value).
        /// </summary>
        /// <param name="value">Проверяемая строка.</param>
        /// <returns>True, если строка имеет значение, иначе false.</returns>
        public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// Обрезает строку до указанной максимальной длины и добавляет суффикс (по умолчанию "...").
        /// </summary>
        /// <param name="value">Строка для обрезки.</param>
        /// <param name="maxLength">Максимальная длина строки.</param>
        /// <param name="truncationSuffix">Суффикс, добавляемый в конце, если строка была обрезана.</param>
        /// <returns>Обрезанная строка или исходная, если ее длина меньше или равна maxLength.</returns>
        public static string Truncate(this string value, int maxLength, string truncationSuffix = "...")
        {
            if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
                return value;

            return string.Concat(value.AsSpan(0, maxLength), truncationSuffix);
        }

        public static string ToBase64(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static string FromBase64(this string base64Value)
        {
            if (string.IsNullOrEmpty(base64Value))
                return base64Value;

            try
            {
                var bytes = Convert.FromBase64String(base64Value);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (FormatException)
            {
                return base64Value;
            }
        }

        private static readonly Regex EmailRegex = new Regex(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Проверяет, является ли строка валидным email-адресом.
        /// </summary>
        /// <param name="email">Строка для проверки.</param>
        /// <returns>True, если строка похожа на email-адрес, иначе false.</returns>
        public static bool IsEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email);
        }
    }
}