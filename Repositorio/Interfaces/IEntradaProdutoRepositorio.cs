using InfraEstrutura.Entidades;
using Repositorio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositório.Interfaces
{
    public interface IEntradaProdutoRepositorio
    {
        void adicionar(EntradaProduto entradaProduto);
        void deletar(EntradaProduto entradaProduto);
        void alterar(EntradaProduto entradaProduto);
        void consultar(EntradaProduto entradaProduto);
    }
}
