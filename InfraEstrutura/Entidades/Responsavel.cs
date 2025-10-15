using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraEstrutura.Entidades
{
    public class Responsavel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Setor { get; set; } = String.Empty;
    }
}
