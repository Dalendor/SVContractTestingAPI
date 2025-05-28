using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVContractTestingAPI.Migrations
{
    /// <inheritdoc />
    public partial class modelfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfMembers",
                table: "Families");

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "IssueDate" },
                values: new object[] { new DateTime(2026, 5, 28, 12, 17, 1, 312, DateTimeKind.Local).AddTicks(1783), new DateTime(2024, 5, 28, 12, 17, 1, 312, DateTimeKind.Local).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpiryDate", "IssueDate" },
                values: new object[] { new DateTime(2026, 5, 28, 12, 17, 1, 312, DateTimeKind.Local).AddTicks(1789), new DateTime(2024, 5, 28, 12, 17, 1, 312, DateTimeKind.Local).AddTicks(1787) });

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpiryDate", "IssueDate" },
                values: new object[] { new DateTime(2026, 5, 28, 12, 17, 1, 312, DateTimeKind.Local).AddTicks(1792), new DateTime(2024, 5, 28, 12, 17, 1, 312, DateTimeKind.Local).AddTicks(1791) });

            migrationBuilder.UpdateData(
                table: "FoodProducts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpiryDate",
                value: new DateTime(2025, 11, 28, 12, 17, 1, 312, DateTimeKind.Local).AddTicks(1634));

            migrationBuilder.UpdateData(
                table: "FoodProducts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpiryDate",
                value: new DateTime(2026, 5, 28, 12, 17, 1, 312, DateTimeKind.Local).AddTicks(1693));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfMembers",
                table: "Families",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "IssueDate" },
                values: new object[] { new DateTime(2026, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2969), new DateTime(2024, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2966) });

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpiryDate", "IssueDate" },
                values: new object[] { new DateTime(2026, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2976), new DateTime(2024, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2974) });

            migrationBuilder.UpdateData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpiryDate", "IssueDate" },
                values: new object[] { new DateTime(2026, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2979), new DateTime(2024, 5, 28, 8, 58, 30, 416, DateTimeKind.Local).AddTicks(2978) });

            migrationBuilder.UpdateData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 1,
                column: "NumberOfMembers",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 2,
                column: "NumberOfMembers",
                value: 3);

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
        }
    }
}
