using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindPlatform.Bff.Workstation.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Renamecreated_idTocreated_ip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created_id",
                table: "trust_devices",
                newName: "created_ip");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created_ip",
                table: "trust_devices",
                newName: "created_id");
        }
    }
}
