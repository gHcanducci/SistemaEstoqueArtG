using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraEstrutura.Migrations
{
    /// <inheritdoc />
    public partial class versao1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_EntradasProdutos_EntradaProdutoId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_SaidasProdutos_SaidaProdutoId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_EntradaProdutoId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_SaidaProdutoId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "EntradaProdutoId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "SaidaProdutoId",
                table: "Produtos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntradaProdutoId",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaidaProdutoId",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EntradaProdutoId",
                table: "Produtos",
                column: "EntradaProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_SaidaProdutoId",
                table: "Produtos",
                column: "SaidaProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_EntradasProdutos_EntradaProdutoId",
                table: "Produtos",
                column: "EntradaProdutoId",
                principalTable: "EntradasProdutos",
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
    }
}
