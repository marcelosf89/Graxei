using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoPlanos : IServicoEntidades<Plano>
    {

        System.Collections.Generic.IList<Plano> GetPlanosAtivos();
    }
}
