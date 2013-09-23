using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoCidades : IServicoEntidades<Cidade>
    {
        Cidade Get(string nome, int idEstado);
        Cidade Get(string nome, Estado estado);
        IList<Cidade> Get(Estado estado);
        IList<Cidade> GetPorEstado(int idEstado);
        IList<Cidade> GetPorSiglaEstado(string sigla);
        IList<Cidade> GetPorNomeEstado(string nome);
    }
}
