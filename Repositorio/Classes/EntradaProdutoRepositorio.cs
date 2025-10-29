using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
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