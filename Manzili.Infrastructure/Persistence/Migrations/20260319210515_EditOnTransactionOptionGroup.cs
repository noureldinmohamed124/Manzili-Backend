using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manzili.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditOnTransactionOptionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionOptions_ServiceOptionGroups_ServiceOptionGroupId",
                table: "TransactionOptions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionOptions_ServiceOptionGroupId",
                table: "TransactionOptions");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TransactionOptions",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "ServiceOptionId",
                table: "TransactionOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionOptions_ServiceOptionId",
                table: "TransactionOptions",
                column: "ServiceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionOptions_ServiceOptions_ServiceOptionId",
                table: "TransactionOptions",
                column: "ServiceOptionId",
                principalTable: "ServiceOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionOptions_ServiceOptions_ServiceOptionId",
                table: "TransactionOptions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionOptions_ServiceOptionId",
                table: "TransactionOptions");

            migrationBuilder.DropColumn(
                name: "ServiceOptionId",
                table: "TransactionOptions");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TransactionOptions",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

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
    }
}
