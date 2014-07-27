using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasEstados
    {
        Estado Get(long id);
        IList<Estado> GetEstados(EstadoOrdem ordem);
    }
}