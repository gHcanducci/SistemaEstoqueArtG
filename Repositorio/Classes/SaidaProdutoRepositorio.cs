using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositório.Interfaces;

namespace Repositorio.Classes
{
    public class SaidaProdutoRepositorio : ISaidaProdutoRepositorio
    {
        private ProjetoContext contexto;
        public SaidaProdutoRepositorio(ProjetoContext contexto)
        {
            this.contexto = contexto;
        }


        public IEnumerable<SaidaProduto> ObterPorProduto(int produtoId)
        {
            return contexto.SaidaProduto
                .Include(s => s.Produto)
                .Include(s => s.Responsavel)
                .Where(s => s.ProdutoId == produtoId)
                .OrderByDescending(s => s.DataSaida)
                .ToList();
        }

        public void adicionar(SaidaProduto saidaProduto)
        {
            contexto.SaidaProduto.Add(saidaProduto);
            contexto.SaveChanges();
        }

        public void deletar(SaidaProduto saidaProduto)
        {
            contexto.SaidaProduto.Remove(saidaProduto);
            contexto.SaveChanges();
        }

        public void alterar(SaidaProduto saidaProduto)
        {
            contexto.SaidaProduto.Update(saidaProduto);
            // perguntar
            contexto.SaveChanges();
        }

        public void consultar(SaidaProduto saidaProduto)
        {
            contexto.SaidaProduto.Find(saidaProduto);
            contexto.SaveChanges();
        }
    }
}