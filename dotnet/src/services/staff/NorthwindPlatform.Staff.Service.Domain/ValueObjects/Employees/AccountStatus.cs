using Common.Core.Results;
using NorthwindPlatform.Staff.Service.Domain.Exceptions;

namespace NorthwindPlatform.Staff.Service.Domain.ValueObjects.Employees
{
    public readonly record struct AccountStatus
    {
        private static readonly HashSet<string> ValidAccountStatus = new(StringComparer.OrdinalIgnoreCase)
        {
            "Active",
            "Deactivate",
            "AwaitingActivation",
            "Suspended",
            "Expired",
            "Archived"
        };

        public static readonly AccountStatus Active = new("Active");
        public static readonly AccountStatus Deactivate = new("Deactivate");
        public static readonly AccountStatus AwaitingActivation = new("AwaitingActivation");
        public static readonly AccountStatus Suspended = new("Suspended"); // Приостановлен
        public static readonly AccountStatus Expired = new("Expired");
        public static readonly AccountStatus Archived = new("Archived");

        public static readonly IReadOnlyList<AccountStatus> AllowedValues = 
        [
            Active, 
            Deactivate, 
            AwaitingActivation, 
            Suspended, 
            Expired, 
            Archived
        ];

        public readonly string Value { get; }

        private AccountStatus(string value)
        {            
            if (string.IsNullOrWhiteSpace(value) || !ValidAccountStatus.Contains(value))
                throw new InvalidAccountStatusException(Error.New(ErrorCode.Create,$"AccountStatus '{value}' is not valid"));

            Value = value;
        }

        public static AccountStatus FromString(string value)
        {
            return new AccountStatus(value);
        }

        public override string ToString() => Value;
        
        public static implicit operator string(AccountStatus value) => value.ToString();
    }
}