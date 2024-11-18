using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace INUPCO.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed_fake_data_for_testing_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4145));

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4149));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "ManufacturerId", "Name", "SubsidiaryId", "TradeCode" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4208), null, null, 1, "Lipitor", 1, "PFE001" },
                    { 2, null, new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4210), null, null, 1, "Xarelto", null, "PFE002" },
                    { 3, null, new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4212), null, null, 2, "Entresto", 2, "NOV001" }
                });

            migrationBuilder.UpdateData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4189));

            migrationBuilder.UpdateData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4190));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

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
    }
}
