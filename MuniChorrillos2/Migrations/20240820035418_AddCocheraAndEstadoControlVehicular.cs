using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuniChorrillos2.Migrations
{
    /// <inheritdoc />
    public partial class AddCocheraAndEstadoControlVehicular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cochera",
                table: "ControlVehicular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ControlVehicular",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cochera",
                table: "ControlVehicular");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ControlVehicular");
        }
    }
}
