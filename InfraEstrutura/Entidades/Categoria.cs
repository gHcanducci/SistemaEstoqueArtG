using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraEstrutura.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = String.Empty;
        public virtual List<Produto> Produto { get; set; } = new List<Produto>();
    }
}
