using System.Security.Principal;

namespace InfraEstrutura.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
        public byte Reposicao { get; set; }

        // NOVO: nome do fornecedor como texto simples
        public string Fornecedor { get; set; } = string.Empty;

        // Navegação
        public Categoria? Categoria { get; set; }

        // Coleções de entradas e saídas relacionadas
        public ICollection<EntradaProduto>? Entradas { get; set; }
        public ICollection<SaidaProduto>? Saidas { get; set; }
    }
}
