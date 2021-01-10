using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class FixedTypoInCreatedByProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CeratedBy",
                table: "Users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "CeratedBy",
                table: "DiaryImages",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "CeratedBy",
                table: "DiaryEntries",
                newName: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Users",
                newName: "CeratedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "DiaryImages",
                newName: "CeratedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "DiaryEntries",
                newName: "CeratedBy");
        }
    }
}
