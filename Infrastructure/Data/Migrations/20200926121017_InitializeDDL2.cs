using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class InitializeDDL2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinhasArquivoExcel");

            migrationBuilder.AlterColumn<string>(
                name: "NomeArquivo",
                table: "ArquivoExcel",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataImportacao",
                table: "ArquivoExcel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MenorDataImportada",
                table: "ArquivoExcel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeTotalItens",
                table: "ArquivoExcel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotalImportado",
                table: "ArquivoExcel",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "LinhaArquivoExcel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdArquivoExcel = table.Column<int>(nullable: false),
                    DataEntrega = table.Column<DateTime>(nullable: false),
                    NomeProduto = table.Column<string>(maxLength: 50, nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    ValorUnitario = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinhaArquivoExcel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinhaArquivoExcel_ArquivoExcel_IdArquivoExcel",
                        column: x => x.IdArquivoExcel,
                        principalTable: "ArquivoExcel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinhaArquivoExcel_IdArquivoExcel",
                table: "LinhaArquivoExcel",
                column: "IdArquivoExcel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinhaArquivoExcel");

            migrationBuilder.DropColumn(
                name: "DataImportacao",
                table: "ArquivoExcel");

            migrationBuilder.DropColumn(
                name: "MenorDataImportada",
                table: "ArquivoExcel");

            migrationBuilder.DropColumn(
                name: "QuantidadeTotalItens",
                table: "ArquivoExcel");

            migrationBuilder.DropColumn(
                name: "ValorTotalImportado",
                table: "ArquivoExcel");

            migrationBuilder.AlterColumn<string>(
                name: "NomeArquivo",
                table: "ArquivoExcel",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "LinhasArquivoExcel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataEntrega = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NomeProduto = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinhasArquivoExcel", x => x.Id);
                });
        }
    }
}
