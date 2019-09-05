using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FortesApi.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Usuario",
                columns: table => new
                {
                    key = table.Column<Guid>(nullable: false),
                    nome = table.Column<string>(maxLength: 250, nullable: false),
                    email = table.Column<string>(maxLength: 250, nullable: false),
                    password = table.Column<string>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Usuario", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "TB_Compras",
                columns: table => new
                {
                    key = table.Column<Guid>(nullable: false),
                    data_compra = table.Column<DateTime>(type: "datetime", nullable: true),
                    descricao = table.Column<string>(maxLength: 100, nullable: false),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    taxa_juros = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    quantidade_parcela = table.Column<int>(nullable: false),
                    valor_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    usuario_key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Compras", x => x.key);
                    table.ForeignKey(
                        name: "FK_TB_Compras_TB_Usuario_usuario_key",
                        column: x => x.usuario_key,
                        principalTable: "TB_Usuario",
                        principalColumn: "key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Compras_usuario_key",
                table: "TB_Compras",
                column: "usuario_key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Compras");

            migrationBuilder.DropTable(
                name: "TB_Usuario");
        }
    }
}
