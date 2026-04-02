using FluentValidation;

namespace NorthwindPlatform.Staff.Service.Application.Features.Employees.Commands.Create
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            
        }
    }
}