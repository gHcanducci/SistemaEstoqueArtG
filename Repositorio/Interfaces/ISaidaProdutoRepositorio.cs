using InfraEstrutura.Entidades;
using Repositorio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositório.Interfaces
{
    public interface ISaidaProdutoRepositorio
    {
        void adicionar(SaidaProduto saidaProduto);
        void deletar(SaidaProduto saidaProduto);
        void alterar(SaidaProduto saidaProduto);
        void consultar(SaidaProduto saidaProduto);
    }
}
