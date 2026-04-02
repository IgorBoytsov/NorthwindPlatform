namespace NorthwindPlatform.Staff.Service.Application.Models.Read
{
    public sealed class EmployeeRead
    {
        public Guid Id {get; set; }
        public Guid UserId { get; set; }
        public string AccountStatus { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Patronymic { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string Citizenship { get;set; } = null!;
    }
}