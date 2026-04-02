namespace NorthwindPlatform.Staff.Service.Domain.ValueObjects.Employees
{
    public readonly record struct EmployeeId
    {
        public readonly Guid Value { get; }

        private EmployeeId(Guid value)
        {
            Value = value;
        }

        public static EmployeeId Create(Guid value)
        {
            return new EmployeeId(value);
        }

        public static EmployeeId New() => new (Guid.CreateVersion7());

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator Guid(EmployeeId value) => value.Value;
        public static implicit operator string(EmployeeId value) => value.Value.ToString();
    }
}