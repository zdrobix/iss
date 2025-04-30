using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDrugStorageIdToStoredDrug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoredDrugs_DrugStorages_DrugStorageId",
                table: "StoredDrugs");

            migrationBuilder.AlterColumn<int>(
                name: "DrugStorageId",
                table: "StoredDrugs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoredDrugs_DrugStorages_DrugStorageId",
                table: "StoredDrugs",
                column: "DrugStorageId",
                principalTable: "DrugStorages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoredDrugs_DrugStorages_DrugStorageId",
                table: "StoredDrugs");

            migrationBuilder.AlterColumn<int>(
                name: "DrugStorageId",
                table: "StoredDrugs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StoredDrugs_DrugStorages_DrugStorageId",
                table: "StoredDrugs",
                column: "DrugStorageId",
                principalTable: "DrugStorages",
                principalColumn: "Id");
        }
    }
}
