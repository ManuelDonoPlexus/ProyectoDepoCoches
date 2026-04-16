using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarDepo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Restructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NIF = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    DateEntry = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EmailAddr = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Makes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HorsePower = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FuelTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Makes_FuelTypes_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "FuelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    License = table.Column<string>(type: "TEXT", nullable: false),
                    KMs = table.Column<int>(type: "INTEGER", nullable: false),
                    ColorId = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    MakeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Dni = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddr = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CarId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drivers_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Payed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CarId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fines_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fines_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarConductors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateDrive = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CarCDId = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverCDId = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarConductors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarConductors_Cars_CarCDId",
                        column: x => x.CarCDId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarConductors_Drivers_DriverCDId",
                        column: x => x.DriverCDId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarConductors_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Rojo" },
                    { 2, "Azul" },
                    { 3, "Verde" },
                    { 4, "Naranja" },
                    { 5, "Amarillo" },
                    { 6, "Celeste" },
                    { 7, "Purpura" },
                    { 8, "Rosa" },
                    { 9, "Negro" },
                    { 10, "Blanco" }
                });

            migrationBuilder.InsertData(
                table: "FuelTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Petroleo" },
                    { 2, "Diesel" },
                    { 3, "Hibrido" },
                    { 4, "Electrico" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "DateEntry", "EmailAddr", "NIF", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateOnly(2007, 3, 14), "jarvis@company.inc", "N79652124", "Jarvis INC", 965123415 },
                    { 2, new DateOnly(2010, 4, 7), "donquer@companiamania.sl", "B12472965", "Donquer SL", 658120205 },
                    { 3, new DateOnly(2008, 5, 21), "luzcar@correo.sa", "A65212479", "Luzcar SA", 912341415 },
                    { 4, new DateOnly(2004, 7, 30), "cantar@ascancions.cb", "E12495276", "Cántar CB", 718916545 }
                });

            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "Id", "FuelTypeId", "HorsePower", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, 163, "Mercedes-Benz A 200", 25632.21m },
                    { 2, 3, 316, "Volvo V60 2.0 T6", 20545.85m },
                    { 3, 2, 118, "Hyundai I20 1.0 TGDI", 16697.64m }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ColorId", "KMs", "License", "MakeId", "OwnerId" },
                values: new object[,]
                {
                    { 1, 7, 123, "8742-GHX", 2, 1 },
                    { 2, 9, 103, "7854-ASD", 1, 3 },
                    { 3, 2, 167, "4152-RTE", 1, 2 },
                    { 4, 1, 263, "6769-QWT", 2, 2 },
                    { 5, 3, 317, "4157-NMD", 3, 1 },
                    { 6, 4, 401, "3437-PLO", 3, 2 },
                    { 7, 5, 320, "6167-AUD", 1, 2 },
                    { 8, 6, 115, "5124-OIY", 2, 1 },
                    { 9, 8, 154, "3112-THE", 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "CarId", "Dni", "EmailAddr", "Name", "OwnerId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 3, "78546932G", null, "Pepino", 2, null },
                    { 2, 2, "47456968F", null, "Lucas", 1, null },
                    { 3, 1, "69321453C", null, "Mario", 3, null },
                    { 4, 1, "49875214L", null, "Ana", 2, null },
                    { 5, 4, "45161314N", null, "Regina", 3, null },
                    { 6, 5, "94563214S", null, "Maria", 1, null }
                });

            migrationBuilder.InsertData(
                table: "CarConductors",
                columns: new[] { "Id", "CarCDId", "DateDrive", "DriverCDId", "DriverId" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2017, 6, 17), 3, null },
                    { 2, 7, new DateOnly(2021, 8, 4), 5, null },
                    { 3, 5, new DateOnly(2019, 11, 22), 4, null },
                    { 4, 9, new DateOnly(2020, 9, 14), 2, null },
                    { 5, 3, new DateOnly(2022, 3, 22), 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarConductors_CarCDId",
                table: "CarConductors",
                column: "CarCDId");

            migrationBuilder.CreateIndex(
                name: "IX_CarConductors_DriverCDId",
                table: "CarConductors",
                column: "DriverCDId");

            migrationBuilder.CreateIndex(
                name: "IX_CarConductors_DriverId",
                table: "CarConductors",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ColorId",
                table: "Cars",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_MakeId",
                table: "Cars",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CarId",
                table: "Drivers",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_OwnerId",
                table: "Drivers",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_CarId",
                table: "Fines",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_OwnerId",
                table: "Fines",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Makes_FuelTypeId",
                table: "Makes",
                column: "FuelTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarConductors");

            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Makes");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "FuelTypes");
        }
    }
}
