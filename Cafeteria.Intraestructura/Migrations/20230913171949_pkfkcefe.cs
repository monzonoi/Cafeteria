using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafeteria.Intraestructura.Migrations
{
    public partial class pkfkcefe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "MateriasPrimas");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Cafes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TiempoPreparacionMinutos",
                table: "Cafes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CafeMateriaPrima",
                columns: table => new
                {
                    CafesId = table.Column<int>(type: "INTEGER", nullable: false),
                    MateriasPrimasId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CafeMateriaPrima", x => new { x.CafesId, x.MateriasPrimasId });
                    table.ForeignKey(
                        name: "FK_CafeMateriaPrima_Cafes_CafesId",
                        column: x => x.CafesId,
                        principalTable: "Cafes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CafeMateriaPrima_MateriasPrimas_MateriasPrimasId",
                        column: x => x.MateriasPrimasId,
                        principalTable: "MateriasPrimas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CafeMateriaPrima_MateriasPrimasId",
                table: "CafeMateriaPrima",
                column: "MateriasPrimasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CafeMateriaPrima");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Cafes");

            migrationBuilder.DropColumn(
                name: "TiempoPreparacionMinutos",
                table: "Cafes");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "MateriasPrimas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
