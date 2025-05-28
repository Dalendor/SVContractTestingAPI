using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SVContractTestingAPI.Migrations
{
    /// <inheritdoc />
    public partial class modelexpansion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FamilyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FamilyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Certificates",
                columns: new[] { "Id", "ExpiryDate", "FamilyId", "IssueDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2969), 1, new DateTime(2024, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2966), "Voedselattest" },
                    { 2, new DateTime(2026, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2976), 1, new DateTime(2024, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2974), "Pamperattest" },
                    { 3, new DateTime(2026, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2979), 2, new DateTime(2024, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2978), "Voedselattest" }
                });

            migrationBuilder.InsertData(
                table: "FamilyMembers",
                columns: new[] { "Id", "DateOfBirth", "FamilyId", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Jan Janssens" },
                    { 2, new DateTime(1982, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Marie Janssens" },
                    { 3, new DateTime(1975, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Tom Peeters" }
                });

            migrationBuilder.UpdateData(
                table: "FoodProducts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2025, 11, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2810));

            migrationBuilder.UpdateData(
                table: "FoodProducts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2026, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2876));

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_FamilyId",
                table: "Certificates",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_FamilyId",
                table: "FamilyMembers",
                column: "FamilyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "FamilyMembers");

            migrationBuilder.UpdateData(
                table: "FoodProducts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2025, 11, 23, 9, 9, 54, 482, DateTimeKind.Local).AddTicks(334));

            migrationBuilder.UpdateData(
                table: "FoodProducts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2026, 5, 23, 9, 9, 54, 482, DateTimeKind.Local).AddTicks(389));
        }
    }
}
