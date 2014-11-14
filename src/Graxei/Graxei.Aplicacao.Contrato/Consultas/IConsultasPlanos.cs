using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasPlanos
    {
        IServicoPlanos ServicoPlanos { get; }
        IList<Plano> GetPlanosAtivos();
    }
}