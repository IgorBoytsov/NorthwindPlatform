namespace Common.Core.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}