using InfraEstrutura.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Interfaces
{
    public interface IResponsavelRepositorio
    {
        public void Adicionar(Responsavel responsavel);

        public void Atualizar(Responsavel responsavel);

        public void Remover(Responsavel responsavel);
    }
}
