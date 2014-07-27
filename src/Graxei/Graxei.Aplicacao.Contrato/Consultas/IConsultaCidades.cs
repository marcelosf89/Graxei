using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasCidades
    {
        IList<Cidade> GetPorEstado(long idEstado);
    }
}