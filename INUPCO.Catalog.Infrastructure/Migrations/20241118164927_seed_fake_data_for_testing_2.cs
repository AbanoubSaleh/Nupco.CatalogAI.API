using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INUPCO.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed_fake_data_for_testing_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 49, 27, 220, DateTimeKind.Utc).AddTicks(3711));

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 49, 27, 220, DateTimeKind.Utc).AddTicks(3714));

            migrationBuilder.UpdateData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 49, 27, 220, DateTimeKind.Utc).AddTicks(3737));

            migrationBuilder.UpdateData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 49, 27, 220, DateTimeKind.Utc).AddTicks(3738));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 38, 17, 931, DateTimeKind.Utc).AddTicks(5545));

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 38, 17, 931, DateTimeKind.Utc).AddTicks(5546));

            migrationBuilder.UpdateData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 38, 17, 931, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 38, 17, 931, DateTimeKind.Utc).AddTicks(5571));
        }
    }
}
