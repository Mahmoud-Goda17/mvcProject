using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.G01.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageNameCoulm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Departments");
        }
    }
}
