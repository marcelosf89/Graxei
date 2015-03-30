using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoPlanos : IServicoEntidades<Plano>
    {
        IList<Plano> GetPlanosAtivos();
    }
}
