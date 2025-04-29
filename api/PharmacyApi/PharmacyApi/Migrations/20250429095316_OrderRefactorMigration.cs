using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyApi.Migrations
{
    /// <inheritdoc />
    public partial class OrderRefactorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderContainers_OrderEntityContainerId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderEntityContainerId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderEntityContainerId1",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderEntityContainerId1",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderEntityContainerId1",
                table: "Orders",
                column: "OrderEntityContainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderContainers_OrderEntityContainerId1",
                table: "Orders",
                column: "OrderEntityContainerId1",
                principalTable: "OrderContainers",
                principalColumn: "Id");
        }
    }
}
