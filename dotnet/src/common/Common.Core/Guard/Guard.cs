namespace Common.Core.Guard
{
    public static class Guard
    {
        /// <summary>
        /// Предоставляет доступ к набору стандартных проверок.
        /// </summary>
        public static GuardClause Against { get; } = new GuardClause();
    }
}