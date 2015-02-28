using Graxei.Modelo.Generico;
using System.Collections.Generic;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioEntidades<T> where T : Entidade
    {
        T GetPorId(long id);
        IList<T> Todos();
    }
}
