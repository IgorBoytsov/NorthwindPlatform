using System.ComponentModel;
using System.Reflection;

namespace Common.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Получает значение из атрибута [Description] для элемента перечисления.
        /// Если атрибут не найден, возвращает стандартное строковое представление элемента (ToString()).
        /// </summary>
        /// <param name="enumValue">Элемент перечисления (this).</param>
        /// <returns>Строка из атрибута [Description] или имя элемента.</returns>
        public static string ToDescriptionString(this Enum enumValue)
        {
            Type type = enumValue.GetType();

            MemberInfo[] memberInfo = type.GetMember(enumValue.ToString());

            if (memberInfo.Length > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                    return ((DescriptionAttribute)attributes[0]).Description;
            }

            return enumValue.ToString();
        }

        /// <summary>
        /// Возвращает словарь, где ключ - это элемент перечисления, а значение - его описание из атрибута [Description].
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        public static Dictionary<T, string> GetValuesWithDescriptions<T>() where T : Enum 
            => GetValues<T>().ToDictionary(enumValue => enumValue, enumValue => enumValue.ToDescriptionString());

        /// <summary>
        /// Пытается получить значение перечисления по строке из его атрибута [Description].
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <param name="description">Строка-описание для поиска.</param>
        /// <returns>Найденное значение перечисления.</returns>
        /// <exception cref="ArgumentException">Если описание не найдено ни у одного элемента.</exception>
        public static T ParseFromDescription<T>(string description) where T : Enum
        {
            foreach (var value in GetValues<T>())
            {
                if (value.ToDescriptionString().Equals(description, StringComparison.OrdinalIgnoreCase))
                    return value;
            }

            throw new ArgumentException($"Нету значений с описанием '{description}' найденных в перечисление {typeof(T).Name}.");
        }

        public static IEnumerable<T> GetValues<T>() where T : Enum => 
            Enum.GetValues(typeof(T)).Cast<T>();
    }
}