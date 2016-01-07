using log4net;

namespace Graxei.Transversais.Comum.LogAplicacao.Log4Net
{
    public class Log4NetImpl : ILogAplicacao
    {
        public void Registrar(string mensagem)
        {
            Registrar("graxei", mensagem);
        }

        
        public void Registrar(string escopo, string mensagem)
        {
            ILog log = LogManager.GetLogger(escopo);
            log.Error(mensagem);
        }
  
    }
}
