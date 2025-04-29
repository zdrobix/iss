using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyApi.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugStorages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugStorages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderContainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderContainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoredDrugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DrugId = table.Column<int>(type: "int", nullable: false),
                    DrugStorageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredDrugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoredDrugs_DrugStorages_DrugStorageId",
                        column: x => x.DrugStorageId,
                        principalTable: "DrugStorages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoredDrugs_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderContainerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospitals_OrderContainers_OrderContainerId",
                        column: x => x.OrderContainerId,
                        principalTable: "OrderContainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageId = table.Column<int>(type: "int", nullable: false),
                    OrderContainerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pharmacies_DrugStorages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "DrugStorages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pharmacies_OrderContainers_OrderContainerId",
                        column: x => x.OrderContainerId,
                        principalTable: "OrderContainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true),
                    HospitalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlacedById = table.Column<int>(type: "int", nullable: false),
                    ResolvedById = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderEntityContainerId = table.Column<int>(type: "int", nullable: true),
                    OrderEntityContainerId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderContainers_OrderEntityContainerId",
                        column: x => x.OrderEntityContainerId,
                        principalTable: "OrderContainers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_OrderContainers_OrderEntityContainerId1",
                        column: x => x.OrderEntityContainerId1,
                        principalTable: "OrderContainers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_PlacedById",
                        column: x => x.PlacedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_ResolvedById",
                        column: x => x.ResolvedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderedDrugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DrugId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedDrugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedDrugs_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedDrugs_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_OrderContainerId",
                table: "Hospitals",
                column: "OrderContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedDrugs_DrugId",
                table: "OrderedDrugs",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedDrugs_OrderId",
                table: "OrderedDrugs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderEntityContainerId",
                table: "Orders",
                column: "OrderEntityContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderEntityContainerId1",
                table: "Orders",
                column: "OrderEntityContainerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PlacedById",
                table: "Orders",
                column: "PlacedById");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ResolvedById",
                table: "Orders",
                column: "ResolvedById");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_OrderContainerId",
                table: "Pharmacies",
                column: "OrderContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_StorageId",
                table: "Pharmacies",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_StoredDrugs_DrugId",
                table: "StoredDrugs",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_StoredDrugs_DrugStorageId",
                table: "StoredDrugs",
                column: "DrugStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HospitalId",
                table: "Users",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PharmacyId",
                table: "Users",
                column: "PharmacyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedDrugs");

            migrationBuilder.DropTable(
                name: "StoredDrugs");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "DrugStorages");

            migrationBuilder.DropTable(
                name: "OrderContainers");
        }
    }
}
