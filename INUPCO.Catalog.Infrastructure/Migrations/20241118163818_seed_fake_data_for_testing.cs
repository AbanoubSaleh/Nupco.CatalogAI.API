using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace INUPCO.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed_fake_data_for_testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Country", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "USA", null, new DateTime(2024, 11, 18, 16, 38, 17, 931, DateTimeKind.Utc).AddTicks(5545), null, null, "Pfizer" },
                    { 2, "Switzerland", null, new DateTime(2024, 11, 18, 16, 38, 17, 931, DateTimeKind.Utc).AddTicks(5546), null, null, "Novartis" }
                });

            migrationBuilder.InsertData(
                table: "Subsidiaries",
                columns: new[] { "Id", "Country", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "ManufacturerId", "Name" },
                values: new object[,]
                {
                    { 1, "Germany", null, new DateTime(2024, 11, 18, 16, 38, 17, 931, DateTimeKind.Utc).AddTicks(5570), null, null, 1, "Pfizer Germany GmbH" },
                    { 2, "Spain", null, new DateTime(2024, 11, 18, 16, 38, 17, 931, DateTimeKind.Utc).AddTicks(5571), null, null, 2, "Novartis Spain SA" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
