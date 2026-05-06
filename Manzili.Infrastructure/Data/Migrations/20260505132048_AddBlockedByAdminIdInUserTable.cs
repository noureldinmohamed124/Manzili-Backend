using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manzili.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBlockedByAdminIdInUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlockedByAdminId",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockedByAdminId",
                table: "Users");
        }
    }
}
