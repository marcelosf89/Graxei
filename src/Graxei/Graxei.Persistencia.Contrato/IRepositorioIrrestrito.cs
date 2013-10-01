using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAST.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioIrrestrito<T> : IRepositorioSalvar<T>, IRepositorioExcluir<T> where T : Entidade
    {
    }
}
