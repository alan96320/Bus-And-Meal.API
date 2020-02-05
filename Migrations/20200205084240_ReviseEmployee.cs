using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMeal.API.Migrations
{
    public partial class ReviseEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employee",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
