using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INUPCO.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class customermapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerGenericItemPharmaCodeMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerSpecificCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenericItemPharmaId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGenericItemPharmaCodeMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGenericItemPharmaCodeMapping_GenericItemPharmas_GenericItemPharmaId",
                        column: x => x.GenericItemPharmaId,
                        principalTable: "GenericItemPharmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGenericItemPharmaCodeMapping_GenericItemPharmaId",
                table: "CustomerGenericItemPharmaCodeMapping",
                column: "GenericItemPharmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerGenericItemPharmaCodeMapping");
        }
    }
}
