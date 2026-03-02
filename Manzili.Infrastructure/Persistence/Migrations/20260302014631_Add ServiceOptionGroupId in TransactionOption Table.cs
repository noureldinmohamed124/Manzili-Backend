using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manzili.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceOptionGroupIdinTransactionOptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceOptionGroupId",
                table: "TransactionOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionOptions_ServiceOptionGroupId",
                table: "TransactionOptions",
                column: "ServiceOptionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionOptions_ServiceOptionGroups_ServiceOptionGroupId",
                table: "TransactionOptions",
                column: "ServiceOptionGroupId",
                principalTable: "ServiceOptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionOptions_ServiceOptionGroups_ServiceOptionGroupId",
                table: "TransactionOptions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionOptions_ServiceOptionGroupId",
                table: "TransactionOptions");

            migrationBuilder.DropColumn(
                name: "ServiceOptionGroupId",
                table: "TransactionOptions");
        }
    }
}
