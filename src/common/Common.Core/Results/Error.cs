namespace Common.Core.Results
{
    public sealed record Error(ErrorCode Code, string Message)
    {
        public static readonly Error None = new(ErrorCode.None, string.Empty);

        public static Error New(ErrorCode code, string message) => new(code, message);

        public static Error Rule(string message) => new(ErrorCode.Rule, message);

        public static Error NotFound(string entityName, object id) => new(ErrorCode.NotFound, $"Сущность '{entityName}' с идентификатором '{id}' не найдена.");
        public static Error Validation(string message) => new(ErrorCode.Validation, message); 
        public static Error Conflict(string message) => new(ErrorCode.Conflict, message); 
        public static Error Server(string message) => new(ErrorCode.Server, message); 
    }
}