

namespace InfraEstrutura.Entidades
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Relacionamentos
        public ICollection<Produto>? Produtos { get; set; }
        public ICollection<EntradaProduto>? EntradasProdutos { get; set; }
    }
}
