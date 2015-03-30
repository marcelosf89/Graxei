using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioPlanos : IRepositorioEntidades<Plano>
    {
        IList<Plano> GetPlanosAtivos();
        Plano GetPlano(long idLoja);
    }
}
