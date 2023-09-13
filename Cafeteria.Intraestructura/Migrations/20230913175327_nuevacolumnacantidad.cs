using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafeteria.Intraestructura.Migrations
{
    public partial class nuevacolumnacantidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad",
                table: "MateriasPrimas",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "MateriasPrimas");
        }
    }
}
