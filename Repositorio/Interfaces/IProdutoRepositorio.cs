using InfraEstrutura.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositório.Interfaces
{
    public interface IProdutoRepositorio
    {
        IEnumerable<Produto> ObterPorNome(string nome);
        IEnumerable<Produto> ObterProdutosParaReposicao();
        IEnumerable<Produto> ObterTodos();

        void adicionar(Produto produto);
        void deletar(Produto produto);
        void alterar(Produto produto);
        void consultar(Produto produto);
    }
}
