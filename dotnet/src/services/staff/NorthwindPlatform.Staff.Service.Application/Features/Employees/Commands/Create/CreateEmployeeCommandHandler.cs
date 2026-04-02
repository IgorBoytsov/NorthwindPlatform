using Common.Core.Results;
using MediatR;
using NorthwindPlatform.Staff.Service.Application.Abstractions;
using NorthwindPlatform.Staff.Service.Domain.Models;
using NorthwindPlatform.Staff.Service.Domain.ValueObjects.Employees;
using Shared.Kernel.Exceptions;

namespace NorthwindPlatform.Staff.Service.Application.Features.Employees.Commands.Create
{
    public sealed class CreateEmployeeCommandHandler(IEventStoreRepository eventStoreRepository) : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;

        public async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = Employee.Create(
                    UserId.Create(request.UserId), 
                    AccountStatus.AwaitingActivation,
                    Name.Create(request.Name),
                    Surname.Create(request.Surname),
                    string.IsNullOrWhiteSpace(request.Patronymic) ? null : Patronymic.Create(request.Patronymic),
                    request.DateOfBirth,
                    Gender.FromString(request.Gender),
                    Citizenship.FromString(request.Citizenship));

                await _eventStoreRepository.SaveAsync<Employee, EmployeeId>($"employee-{employee.Id}", employee, -1, cancellationToken);

                return Result.Success();       
            }
            catch(DomainException ex)
            {
                return Result.Failure(ex.Error);
            }
            catch (System.Exception ex)
            {
                return Result.Failure(Error.New(ErrorCode.Domain, ex.ToString()));
            }
        }
    }
}