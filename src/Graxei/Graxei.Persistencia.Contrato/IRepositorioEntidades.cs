using FAST.Modelo;
using System.Collections.Generic;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioEntidades<T> : IRepositorioSalvar<T>, IRepositorioExcluir<T> where T : Entidade
    {
        T GetPorId(long id);
        IList<T> Todos();
    }
}
