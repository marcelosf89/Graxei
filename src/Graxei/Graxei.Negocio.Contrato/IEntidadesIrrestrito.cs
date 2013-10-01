using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAST.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IEntidadesIrrestrito<T> : ISalvarEntidade<T>, IExcluirEntidade<T> where T : Entidade
    {
    }
}
