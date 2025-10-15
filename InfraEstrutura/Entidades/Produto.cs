using System.Security.Principal;

namespace InfraEstrutura.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = String.Empty;
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
        public byte Reposicao { get; set; }
        public int EntradaProdutoId { get; set; }
        public int SaidaProdutoId { get; set; }
        public int FornecedorId { get; set; }

        
        public virtual Categoria Categoria { get; set; }
        public virtual EntradaProduto EntradaProduto { get; set; }
        public virtual SaidaProduto SaidaProduto { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
