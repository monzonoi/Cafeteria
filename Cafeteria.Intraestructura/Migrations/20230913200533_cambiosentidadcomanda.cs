using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafeteria.Intraestructura.Migrations
{
    public partial class cambiosentidadcomanda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Comandas_ComandaId1",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ComandaId1",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ComandaId1",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "Comandas",
                newName: "UsuarioId");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Comandas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ComandaId",
                table: "Pedidos",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comandas_UsuarioId",
                table: "Comandas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comandas_Usuarios_UsuarioId",
                table: "Comandas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Comandas_ComandaId",
                table: "Pedidos",
                column: "ComandaId",
                principalTable: "Comandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comandas_Usuarios_UsuarioId",
                table: "Comandas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Comandas_ComandaId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ComandaId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Comandas_UsuarioId",
                table: "Comandas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Comandas");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Comandas",
                newName: "PedidoId");

            migrationBuilder.AddColumn<int>(
                name: "ComandaId1",
                table: "Pedidos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ComandaId1",
                table: "Pedidos",
                column: "ComandaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Comandas_ComandaId1",
                table: "Pedidos",
                column: "ComandaId1",
                principalTable: "Comandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
