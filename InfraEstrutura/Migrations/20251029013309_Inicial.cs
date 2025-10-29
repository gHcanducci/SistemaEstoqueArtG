using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraEstrutura.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsáveis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Setor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NivelAcesso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsáveis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntradasProdutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    FornecedorId = table.Column<int>(type: "int", nullable: false),
                    ResponsavelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradasProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntradasProdutos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntradasProdutos_Responsáveis_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Responsáveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Reposicao = table.Column<byte>(type: "tinyint", nullable: false),
                    FornecedorId = table.Column<int>(type: "int", nullable: false),
                    EntradaProdutoId = table.Column<int>(type: "int", nullable: false),
                    SaidaProdutoId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId1",
                        column: x => x.CategoriaId1,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produtos_EntradasProdutos_EntradaProdutoId",
                        column: x => x.EntradaProdutoId,
                        principalTable: "EntradasProdutos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produtos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaidasProdutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoSaida = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    ResponsavelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaidasProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaidasProdutos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaidasProdutos_Responsáveis_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Responsáveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Responsáveis",
                columns: new[] { "Id", "NivelAcesso", "Nome", "SenhaHash", "Setor" },
                values: new object[] { 1, "admin", "Administrador", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", "Administracao" });

            migrationBuilder.CreateIndex(
                name: "IX_EntradasProdutos_FornecedorId",
                table: "EntradasProdutos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasProdutos_ProdutoId",
                table: "EntradasProdutos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradasProdutos_ResponsavelId",
                table: "EntradasProdutos",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId1",
                table: "Produtos",
                column: "CategoriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EntradaProdutoId",
                table: "Produtos",
                column: "EntradaProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_SaidaProdutoId",
                table: "Produtos",
                column: "SaidaProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_SaidasProdutos_ProdutoId",
                table: "SaidasProdutos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_SaidasProdutos_ResponsavelId",
                table: "SaidasProdutos",
                column: "ResponsavelId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasProdutos_Produtos_ProdutoId",
                table: "EntradasProdutos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_SaidasProdutos_SaidaProdutoId",
                table: "Produtos",
                column: "SaidaProdutoId",
                principalTable: "SaidasProdutos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradasProdutos_Fornecedores_FornecedorId",
                table: "EntradasProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Fornecedores_FornecedorId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_EntradasProdutos_Produtos_ProdutoId",
                table: "EntradasProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_SaidasProdutos_Produtos_ProdutoId",
                table: "SaidasProdutos");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "EntradasProdutos");

            migrationBuilder.DropTable(
                name: "SaidasProdutos");

            migrationBuilder.DropTable(
                name: "Responsáveis");
        }
    }
}
