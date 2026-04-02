using Shared.Kernel.Primitives;

namespace NorthwindPlatform.Staff.Service.Domain.Events.Employees
{
    public sealed record EmployeeCreated(
        string Id,
        string UserId, 
        string AccountStatus, 
        string Name, string Surname, string? Patronymic, 
        string DateOfBirth, 
        string Gender, 
        string Citizenship) : IDomainEvent;
}