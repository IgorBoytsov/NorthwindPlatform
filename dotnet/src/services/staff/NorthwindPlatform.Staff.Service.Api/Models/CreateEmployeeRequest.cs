namespace NorthwindPlatform.Staff.Service.Api.Models
{
    public sealed record CreateEmployeeRequest(string UserId, string Name, string Surname, string? Patronymic, string DateOfBirth, string Gender, string Citizenship);
}