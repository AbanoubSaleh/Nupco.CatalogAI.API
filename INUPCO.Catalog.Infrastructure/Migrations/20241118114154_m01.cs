using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INUPCO.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGenericItemPharmaCodeMapping_GenericItemPharmas_GenericItemPharmaId",
                table: "CustomerGenericItemPharmaCodeMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerGenericItemPharmaCodeMapping",
                table: "CustomerGenericItemPharmaCodeMapping");

            migrationBuilder.RenameTable(
                name: "CustomerGenericItemPharmaCodeMapping",
                newName: "CustomerGenericItemPharmaCodeMappings");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerGenericItemPharmaCodeMapping_GenericItemPharmaId",
                table: "CustomerGenericItemPharmaCodeMappings",
                newName: "IX_CustomerGenericItemPharmaCodeMappings_GenericItemPharmaId");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerSpecificCode",
                table: "CustomerGenericItemPharmaCodeMappings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerCode",
                table: "CustomerGenericItemPharmaCodeMappings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerGenericItemPharmaCodeMappings",
                table: "CustomerGenericItemPharmaCodeMappings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGenericItemPharmaCodeMappings_CustomerCode_CustomerSpecificCode",
                table: "CustomerGenericItemPharmaCodeMappings",
                columns: new[] { "CustomerCode", "CustomerSpecificCode" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGenericItemPharmaCodeMappings_GenericItemPharmas_GenericItemPharmaId",
                table: "CustomerGenericItemPharmaCodeMappings",
                column: "GenericItemPharmaId",
                principalTable: "GenericItemPharmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGenericItemPharmaCodeMappings_GenericItemPharmas_GenericItemPharmaId",
                table: "CustomerGenericItemPharmaCodeMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerGenericItemPharmaCodeMappings",
                table: "CustomerGenericItemPharmaCodeMappings");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGenericItemPharmaCodeMappings_CustomerCode_CustomerSpecificCode",
                table: "CustomerGenericItemPharmaCodeMappings");

            migrationBuilder.RenameTable(
                name: "CustomerGenericItemPharmaCodeMappings",
                newName: "CustomerGenericItemPharmaCodeMapping");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerGenericItemPharmaCodeMappings_GenericItemPharmaId",
                table: "CustomerGenericItemPharmaCodeMapping",
                newName: "IX_CustomerGenericItemPharmaCodeMapping_GenericItemPharmaId");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerSpecificCode",
                table: "CustomerGenericItemPharmaCodeMapping",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerCode",
                table: "CustomerGenericItemPharmaCodeMapping",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerGenericItemPharmaCodeMapping",
                table: "CustomerGenericItemPharmaCodeMapping",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGenericItemPharmaCodeMapping_GenericItemPharmas_GenericItemPharmaId",
                table: "CustomerGenericItemPharmaCodeMapping",
                column: "GenericItemPharmaId",
                principalTable: "GenericItemPharmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
