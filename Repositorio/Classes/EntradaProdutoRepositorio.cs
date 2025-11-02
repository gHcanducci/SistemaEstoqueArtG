using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositório.Interfaces;

namespace Repositorio.Classes
{
    public class EntradaProdutoRepositorio : IEntradaProdutoRepositorio
    {
        private ProjetoContext contexto;
        public EntradaProdutoRepositorio(ProjetoContext contexto)
        {
            this.contexto = contexto;
        }

        public IEnumerable<EntradaProduto> ObterPorProduto(int produtoId)
        {
            return contexto.EntradaProduto
                .Include(e => e.Produto)
                .Include(e => e.Responsavel)
                .Where(e => e.ProdutoId == produtoId)
                .OrderByDescending(e => e.DataEntrada)
                .ToList();
        }

        public void adicionar(EntradaProduto entradaProduto)
        {
            contexto.EntradaProduto.Add(entradaProduto);
            contexto.SaveChanges();
        }

        public void deletar(EntradaProduto entradaProduto)
        {
            contexto.EntradaProduto.Remove(entradaProduto);
            contexto.SaveChanges();
        }

        public void alterar(EntradaProduto entradaProduto)
        {
            contexto.EntradaProduto.Update(entradaProduto);
            // perguntar
            contexto.SaveChanges();
        }

        public void consultar(EntradaProduto entradaProduto)
        {
            contexto.EntradaProduto.Find(entradaProduto);
            contexto.SaveChanges();
        }
    }
}