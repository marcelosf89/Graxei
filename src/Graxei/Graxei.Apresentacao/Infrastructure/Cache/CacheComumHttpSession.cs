using Graxei.Apresentacao.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.Models;
using System.Web;

namespace Graxei.Apresentacao.Infrastructure.Cache
{
    public class CacheComumHttpSession : ICacheComum
    {
        public CacheComumHttpSession()
        {
            _session = new HttpSessionStateWrapper(HttpContext.Current.Session);
        }

        public CacheComumHttpSession(HttpSessionStateBase httpSessionStateBase)
        {
            _session = httpSessionStateBase;
        }

        public IpRegiaoModel IpRegiaoModel { 
            get
            {
                IpRegiaoModel result =  (IpRegiaoModel)_session[ChavesSessao.IpRegiao];
                if (result == null)
                {
                    result = new IpRegiaoModel { Cidade = string.Empty, Pais = string.Empty };
                    IpRegiaoModel = result;
                }

                return result;
            } 
            set
            {
                _session[ChavesSessao.IpRegiao] = value;
            } 
        }

        private HttpSessionStateBase _session;
    }
}