using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linqe.Migrations
{
    /// <inheritdoc />
    public partial class updateempolyee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empolyee_Department_DeptId",
                table: "Empolyee");

            migrationBuilder.AlterColumn<int>(
                name: "DeptId",
                table: "Empolyee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Empolyee_Department_DeptId",
                table: "Empolyee",
                column: "DeptId",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empolyee_Department_DeptId",
                table: "Empolyee");

            migrationBuilder.AlterColumn<int>(
                name: "DeptId",
                table: "Empolyee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Empolyee_Department_DeptId",
                table: "Empolyee",
                column: "DeptId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
