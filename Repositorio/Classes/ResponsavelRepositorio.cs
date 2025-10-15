using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositório.Interfaces;
using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using Repositorio.Interfaces;


namespace Repositorio.Classes
{
    public class ResponsavelRepositorio : IResponsavelRepositorio
    {
        private ProjetoContext contexto;
        public ResponsavelRepositorio(ProjetoContext contexto)
        {
            this.contexto = contexto;
        }

        public void cadastrar(Responsavel responsavel)
        {
            contexto.Responsaveis.Add(responsavel);
            contexto.SaveChanges();
            
        }

        public void alterar(Responsavel responsavel)
        {
            contexto.Responsaveis.Update(responsavel);
            // --------- PERGUNTAR AO PROFESSOR SOBRE A ESTRUTURA DO UPDATE
            contexto.SaveChanges();
        }
    }
}
