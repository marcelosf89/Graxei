using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum.LogAplicacao.Log4Net
{
    public class Log4NetImpl : ILogAplicacao
    {
        public void Registrar(string mensagem)
        {
            Registrar("graxei");
        }

        
        public void Registrar(string escopo, string mensagem)
        {
            ILog log = LogManager.GetLogger(escopo);
            log.Error(mensagem);
        }
  
    }
}
