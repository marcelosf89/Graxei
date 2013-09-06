using FAST.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioEntidades<T> where T : Entidade
    {
        void Salvar(T t);
        void Excluir(T t);
        T GetPorId(long id);
    }
}
