using FAST.Modelo;
using System.Collections.Generic;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioEntidades<T> where T : Entidade
    {
        void Salvar(T t);
        void Excluir(T t);
        T GetPorId(long id);
        IList<T> Todos();
    }
}
