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
            public int? FornecedorId { get; set; }

            // Navegação
            public Categoria? Categoria { get; set; }
            public Fornecedor? Fornecedor { get; set; }

            // Coleções de entradas e saídas relacionadas
            public ICollection<EntradaProduto>? Entradas { get; set; }
            public ICollection<SaidaProduto>? Saidas { get; set; }
        }
}
