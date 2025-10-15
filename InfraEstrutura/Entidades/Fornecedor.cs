using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraEstrutura.Entidades
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = String.Empty;
        public string Telefone { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public int EntradaProdutoId { get; set; }
        
        public virtual required EntradaProduto EntradaProduto { get; set; }
        public virtual required Produto Produto { get; set; }

    }
}
