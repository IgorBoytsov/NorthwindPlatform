using Common.Core.Results;
using MediatR;

namespace NorthwindPlatform.Staff.Service.Application.Features.Employees.Commands.Create
{
    public sealed record CreateEmployeeCommand(Guid UserId, string Name, string Surname, string? Patronymic, DateOnly DateOfBirth, string Gender, string Citizenship) : IRequest<Result>;
}