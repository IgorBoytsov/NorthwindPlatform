using NorthwindPlatform.Staff.Service.Application.Models.Read;

namespace NorthwindPlatform.Staff.Service.Application.Abstractions.Repositories
{
    public interface IEmployeeReadRepository
    {
        Task UpsertAsync(EmployeeRead employee, CancellationToken ct);
    }
}