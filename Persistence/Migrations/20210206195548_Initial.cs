using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingService.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Parking",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingLevel",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    ParkingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingLevel_Parking_ParkingId",
                        column: x => x.ParkingId,
                        principalSchema: "dbo",
                        principalTable: "Parking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpace",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    VehicleLicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleWeight = table.Column<int>(type: "int", nullable: true),
                    FloorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSpace_ParkingLevel_FloorId",
                        column: x => x.FloorId,
                        principalSchema: "dbo",
                        principalTable: "ParkingLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLevel_ParkingId",
                schema: "dbo",
                table: "ParkingLevel",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpace_FloorId",
                schema: "dbo",
                table: "ParkingSpace",
                column: "FloorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpace",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ParkingLevel",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Parking",
                schema: "dbo");
        }
    }
}
