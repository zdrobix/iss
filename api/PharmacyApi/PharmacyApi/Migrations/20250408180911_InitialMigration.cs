using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageId = table.Column<int>(type: "int", nullable: false)
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: true),
                    PharmacyId = table.Column<int>(type: "int", nullable: true)
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
                name: "PlacedOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlacedById = table.Column<int>(type: "int", nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: true),
                    PharmacyId = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacedOrders_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlacedOrders_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlacedOrders_Users_PlacedById",
                        column: x => x.PlacedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResolvedOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResolvedById = table.Column<int>(type: "int", nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: true),
                    PharmacyId = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResolvedOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResolvedOrders_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResolvedOrders_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResolvedOrders_Users_ResolvedById",
                        column: x => x.ResolvedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedDrugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DrugId = table.Column<int>(type: "int", nullable: false),
                    PlacedOrderId = table.Column<int>(type: "int", nullable: true),
                    ResolvedOrderId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_OrderedDrugs_PlacedOrders_PlacedOrderId",
                        column: x => x.PlacedOrderId,
                        principalTable: "PlacedOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderedDrugs_ResolvedOrders_ResolvedOrderId",
                        column: x => x.ResolvedOrderId,
                        principalTable: "ResolvedOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedDrugs_DrugId",
                table: "OrderedDrugs",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedDrugs_PlacedOrderId",
                table: "OrderedDrugs",
                column: "PlacedOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedDrugs_ResolvedOrderId",
                table: "OrderedDrugs",
                column: "ResolvedOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_StorageId",
                table: "Pharmacies",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedOrders_HospitalId",
                table: "PlacedOrders",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedOrders_PharmacyId",
                table: "PlacedOrders",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedOrders_PlacedById",
                table: "PlacedOrders",
                column: "PlacedById");

            migrationBuilder.CreateIndex(
                name: "IX_ResolvedOrders_HospitalId",
                table: "ResolvedOrders",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolvedOrders_PharmacyId",
                table: "ResolvedOrders",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_ResolvedOrders_ResolvedById",
                table: "ResolvedOrders",
                column: "ResolvedById");

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
                name: "PlacedOrders");

            migrationBuilder.DropTable(
                name: "ResolvedOrders");

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
        }
    }
}
