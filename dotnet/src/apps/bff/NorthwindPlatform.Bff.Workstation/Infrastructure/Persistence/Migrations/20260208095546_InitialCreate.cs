using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindPlatform.Bff.Workstation.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trust_devices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    device_id = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    device_fingerprint_hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    auth_service_session_id = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    first_seen_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_activity_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_id = table.Column<string>(type: "TEXT", nullable: true),
                    lat_id = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trust_devices", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trust_devices_auth_service_session_id",
                table: "trust_devices",
                column: "auth_service_session_id");

            migrationBuilder.CreateIndex(
                name: "IX_trust_devices_user_id_device_id",
                table: "trust_devices",
                columns: new[] { "user_id", "device_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trust_devices");
        }
    }
}
