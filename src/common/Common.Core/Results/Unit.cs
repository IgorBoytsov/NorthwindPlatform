namespace Common.Core.Results
{
    /// <summary>
    /// Представляет "пустое" значение для дженериков.
    /// </summary>
    public readonly struct Unit
    {
        public static readonly Unit Value = new();
    }
}