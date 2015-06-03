using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Transversais.Comum.Entidades;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultaEstados
    {
        Estado Get(long id);
        IList<Estado> GetEstados(EstadoOrdem ordem);
    }
}