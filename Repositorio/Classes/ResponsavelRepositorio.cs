using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositório.Interfaces;
using InfraEstrutura.Data;
using InfraEstrutura.Entidades;
using Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Repositorio.Classes
{
    public class ResponsavelRepositorio : IResponsavelRepositorio
    {
        private ProjetoContext contexto;
        public ResponsavelRepositorio(ProjetoContext contexto)
        {
            this.contexto = contexto;
        }

        public bool ExisteVinculos(int responsavelId)
        {
            return contexto.EntradaProduto.Any(e => e.ResponsavelId == responsavelId) ||
                   contexto.SaidaProduto.Any(s => s.ResponsavelId == responsavelId);
        }
        public IEnumerable<Responsavel> ObterTodos() => contexto.Responsaveis.ToList();

        public void Adicionar(Responsavel responsavel) => contexto.Responsaveis.Add(responsavel);

        public void Atualizar(Responsavel responsavel) => contexto.Responsaveis.Update(responsavel);

        public void Remover(Responsavel responsavel) => contexto.Responsaveis.Remove(responsavel);

        public void Salvar() => contexto.SaveChanges();
    }
}
