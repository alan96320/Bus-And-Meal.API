using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMeal.API.Migrations
{
    public partial class departmentFluent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Department_Code",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_Name",
                table: "Department");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Department_Code",
                table: "Department",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_Name",
                table: "Department",
                column: "Name",
                unique: true);
        }
    }
}
