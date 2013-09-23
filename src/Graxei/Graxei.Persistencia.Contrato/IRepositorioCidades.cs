using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioCidades : IRepositorioEntidades<Cidade>
    {
        Cidade Get(string nome, int idEstado);
        Cidade Get(string nome, Estado estado);
        IList<Cidade> Get(Estado estado);
        IList<Cidade> GetPorEstado(int idEstado);
        IList<Cidade> GetPorSiglaEstado(string sigla);
        IList<Cidade> GetPorNomeEstado(string nome);
    }
}
