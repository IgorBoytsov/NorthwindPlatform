namespace Common.Core.Results
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Выполняет следующую асинхронную операцию, если предыдущая завершилась успешно.
        /// Если предыдущая операция провалилась, ее ошибка пробрасывается дальше.
        /// </summary>
        /// <typeparam name="TIn">Тип успешного результата на входе.</typeparam>
        /// <typeparam name="TOut">Тип успешного результата на выходе.</typeparam>
        /// <param name="resultTask">Задача, возвращающая результат предыдущей операции.</param>
        /// <param name="next">Функция, которую нужно выполнить в случае успеха.</param>
        /// <returns>Результат выполнения функции 'next' или исходная ошибка.</returns>
        public static async Task<Result<TOut>> Then<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<Result<TOut>>> next)
        {
            Result<TIn> result = await resultTask;

            if (result.IsFailure)
            {
                return Result<TOut>.Failure(result.Errors);
            }

            return await next(result.Value);
        }

        /// <summary>
        /// Синхронный аналог Then. Выполняет следующую операцию, если предыдущая завершилась успешно.
        /// </summary>
        public static Result<TOut> Then<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Result<TOut>> next)
        {
            return result.IsSuccess
                ? next(result.Value)
                : Result<TOut>.Failure(result.Errors);
        }

        /// <summary>
        /// Преобразует успешное значение результата, не меняя состояние ошибки.
        /// Полезно для маппинга DTO в доменные модели и наоборот.
        /// </summary>
        public static Result<TOut> Map<TIn, TOut>(
             this Result<TIn> result,
             Func<TIn, TOut> mapper)
        {
            return result.IsSuccess
                ? Result<TOut>.Success(mapper(result.Value))
                : Result<TOut>.Failure(result.Errors);
        }

        /// <summary>
        /// Позволяет выполнить асинхронное сопоставление (Match) для результата, обернутого в Task.
        /// </summary>
        public static async Task<TOut> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> onSuccess,
            Func<IReadOnlyList<Error>, TOut> onFailure)
        {
            var result = await resultTask;
            return result.Match(onSuccess, onFailure);
        }
    }
}