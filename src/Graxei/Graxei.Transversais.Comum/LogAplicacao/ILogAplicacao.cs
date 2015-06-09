using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum.LogAplicacao
{
    public interface ILogAplicacao
    {
        void Registrar(string mensagem);
        void Registrar(string escopo, string mensagem);
    }
}
