using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraEstrutura.Entidades
{
    public class SaidaProduto
    {
        public int Id { get; set; }
        public DateTime DataSaida { get; set; }
        public string TipoSaida { get; set; } = string.Empty;
        public int Quantidade { get; set; }

        public int ProdutoId { get; set; }
        public int ResponsavelId { get; set; }

        // Navegação
        public Produto? Produto { get; set; }
        public Responsavel? Responsavel { get; set; }

        // Relacionamento reverso
        public ICollection<Produto>? Produtos { get; set; }
    }
}
