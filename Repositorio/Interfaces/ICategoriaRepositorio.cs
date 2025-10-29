using InfraEstrutura.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositório.Interfaces
{
    public interface ICategoriaRepositorio
    {
        void adicionar(Categoria categoria);
        void deletar(Categoria categoria);
        void alterar(Categoria categoria);
        void consultar(Categoria categoria);
    }
}
