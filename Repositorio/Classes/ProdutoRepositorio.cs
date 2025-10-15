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
            // --------- PERGUNTAR A PROFESSORA
            contexto.SaveChanges();
        }
        public void consultar(Produto produto)
        {
            contexto.Produtos.Find(produto);
        }
    }
}
