using Repositório.Interfaces;
using InfraEstrutura.Data;
using InfraEstrutura.Entidades;

namespace Repositório.Classes
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private ProjetoContext contexto;
        public CategoriaRepositorio(ProjetoContext contexto) 
        {
            this.contexto = contexto;
        }

        public void adicionar(Categoria categoria)
        {
            contexto.Categorias.Add(categoria);
            contexto.SaveChanges();
        }

        public void deletar(Categoria categoria)
        {
            contexto.Categorias.Remove(categoria);
            contexto.SaveChanges();
        }
        public void alterar(Categoria categoria)
        {
            contexto.Categorias.Update(categoria);
            // --------- PERGUNTAR A PROFESSORA
            contexto.SaveChanges();
        }
        public void consultar(Categoria categoria)
        {
            contexto.Categorias.Find(categoria);
        }
    }
}
