using System.Data;
using Dapper;
using NorthwindPlatform.Staff.Service.Application.Abstractions.Repositories;
using NorthwindPlatform.Staff.Service.Application.Models.Read;
using NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Helpers;

namespace NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Repositories.Employee
{
    public sealed class EmployeeReadRepository(IDbConnection connection) : IEmployeeReadRepository
    {
        private readonly IDbConnection _connection = connection;
        private const string repo = "EmployeeRead";

        // private readonly string _getOverviewEmployee = SqlLoader.Load(repo, "GetOverviewEmployee");

        public async Task UpsertAsync(EmployeeRead employee, CancellationToken ct)
        {
            var sql = @"
                INSERT INTO employees (
                    id, user_id, account_status, name, surname, patronymic, 
                    date_of_birth, gender, citizenship
                )
                VALUES (
                    @Id, @UserId, @AccountStatus, @Name, @Surname, @Patronymic, 
                    @DateOfBirth, @Gender, @Citizenship
                )
                ON CONFLICT (user_id) DO UPDATE SET
                    id = EXCLUDED.id,
                    account_status = EXCLUDED.account_status,
                    name = EXCLUDED.name,
                    surname = EXCLUDED.surname,
                    patronymic = EXCLUDED.patronymic,
                    date_of_birth = EXCLUDED.date_of_birth,
                    gender = EXCLUDED.gender,
                    citizenship = EXCLUDED.citizenship;
            ";
    
            await _connection.ExecuteAsync(sql, employee);
        }
    }
}