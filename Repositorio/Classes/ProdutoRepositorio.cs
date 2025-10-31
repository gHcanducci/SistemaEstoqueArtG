using Repositório.Interfaces;
using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Repositório.Classes
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private ProjetoContext contexto;
        public ProdutoRepositorio(ProjetoContext contexto) 
        {
            this.contexto = contexto;
        }

        public IEnumerable<Produto> ObterPorNome(string nome)
        {
            return contexto.Produtos
                .Where(p => p.Nome.Contains(nome))
                .ToList();
        }

        public IEnumerable<Produto> ObterProdutosParaReposicao()
        {
            // Filtra produtos com Reposicao == 1
            return contexto.Produtos
                .Where(p => p.Reposicao == 1)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Produto> ObterTodos()
        {
            return contexto.Produtos
                .AsNoTracking()
                .ToList();
        }


        public void adicionar(Produto produto)
        {
            contexto.Produtos.Add(produto);
            contexto.SaveChanges();
        }

        public void deletar(Produto produto)
        { 
                contexto.Produtos.Remove(produto);
                contexto.SaveChanges();
        }
        public void alterar(Produto produto)
        {
            contexto.Produtos.Update(produto);
            contexto.SaveChanges();
        }
        public void consultar(Produto produto)
        {
            contexto.Produtos.Find(produto);
        }
    }
}
