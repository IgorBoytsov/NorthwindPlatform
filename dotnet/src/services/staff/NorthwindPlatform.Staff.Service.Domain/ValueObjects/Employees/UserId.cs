namespace NorthwindPlatform.Staff.Service.Domain.ValueObjects.Employees
{
    public readonly record struct UserId
    {
        public readonly Guid Value { get; }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId Create(Guid value)
        {
            return new UserId(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator Guid(UserId value) => value.Value;
        public static implicit operator string(UserId value) => value.Value.ToString();
    }
}