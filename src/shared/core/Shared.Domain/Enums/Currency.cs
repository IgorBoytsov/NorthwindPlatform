namespace Shared.Domain.Enums
{
    public enum Currency
    {
        /// <summary>
        /// Представляет собой неопределенную валюту или валюту по умолчанию. Не использовать для транзакций.
        /// </summary>
        None = 0,

        /// <summary>
        /// Российский рубль (₽).
        /// </summary>
        RUB = 1,

        /// <summary>
        /// USA доллар ($).
        /// </summary>
        USD = 2,

        /// <summary>
        /// Евро (€).
        /// </summary>
        EUR = 3
    }
}