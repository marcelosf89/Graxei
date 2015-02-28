using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioPlanos : IRepositorioEntidades<Plano>
    {
        System.Collections.Generic.IList<Plano> GetPlanosAtivos();
    }
}
