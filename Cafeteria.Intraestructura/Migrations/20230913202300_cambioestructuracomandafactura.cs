using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafeteria.Intraestructura.Migrations
{
    public partial class cambioestructuracomandafactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesFactura_Cafes_CafeId",
                table: "DetallesFactura");

            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Pedidos_PedidoId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_PedidoId",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_DetallesFactura_CafeId",
                table: "DetallesFactura");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CafeId",
                table: "DetallesFactura");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Pedidos",
                newName: "Cantidad");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Pedidos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "CafeId",
                table: "Pedidos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComandaId",
                table: "Facturas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CafeId",
                table: "Pedidos",
                column: "CafeId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ComandaId",
                table: "Facturas",
                column: "ComandaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Comandas_ComandaId",
                table: "Facturas",
                column: "ComandaId",
                principalTable: "Comandas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Cafes_CafeId",
                table: "Pedidos",
                column: "CafeId",
                principalTable: "Cafes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Comandas_ComandaId",
                table: "Facturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Cafes_CafeId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_CafeId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_ComandaId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "CafeId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ComandaId",
                table: "Facturas");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Pedidos",
                newName: "UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Pedidos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Pedidos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CafeId",
                table: "DetallesFactura",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_PedidoId",
                table: "Facturas",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesFactura_CafeId",
                table: "DetallesFactura",
                column: "CafeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesFactura_Cafes_CafeId",
                table: "DetallesFactura",
                column: "CafeId",
                principalTable: "Cafes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Pedidos_PedidoId",
                table: "Facturas",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
