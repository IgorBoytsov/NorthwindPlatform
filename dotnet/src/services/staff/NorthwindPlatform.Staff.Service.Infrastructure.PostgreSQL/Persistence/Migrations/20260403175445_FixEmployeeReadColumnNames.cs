using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindPlatform.Staff.Service.Infrastructure.PostgreSQL.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixEmployeeReadColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "employees",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "employees",
                newName: "patronymic");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "employees",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "employees",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Citizenship",
                table: "employees",
                newName: "citizenship");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "employees",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "employees",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "employees",
                newName: "date_of_birth");

            migrationBuilder.RenameColumn(
                name: "AccountStatus",
                table: "employees",
                newName: "account_status");

            migrationBuilder.RenameIndex(
                name: "IX_employees_UserId",
                table: "employees",
                newName: "IX_employees_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "employees",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "patronymic",
                table: "employees",
                newName: "Patronymic");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "employees",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "employees",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "citizenship",
                table: "employees",
                newName: "Citizenship");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "employees",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "employees",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "date_of_birth",
                table: "employees",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "account_status",
                table: "employees",
                newName: "AccountStatus");

            migrationBuilder.RenameIndex(
                name: "IX_employees_user_id",
                table: "employees",
                newName: "IX_employees_UserId");
        }
    }
}
