using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindPlatform.Bff.Workstation.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Renamelat_idTolast_ip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lat_id",
                table: "trust_devices",
                newName: "last_ip");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "last_ip",
                table: "trust_devices",
                newName: "lat_id");
        }
    }
}
