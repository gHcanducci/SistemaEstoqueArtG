using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraEstrutura.Entidades
{
    public class EntradaProduto
    {
        public int Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public int Quantidade { get; set; }

        public int ProdutoId { get; set; }
        public int FornecedorId { get; set; }
        public int ResponsavelId { get; set; }

        // Navegação
        public Produto? Produto { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        public Responsavel? Responsavel { get; set; }

        // Relacionamento reverso
        public ICollection<Produto>? Produtos { get; set; }
    }
}
