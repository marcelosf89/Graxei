using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Graxei.Apresentacao.Areas.Administrativo.Infraestutura.Cache
{
    public class CacheEnderecosSessaoHttp : ICacheElementosEndereco
    {
        public CacheEnderecosSessaoHttp()
        {
            _session = new HttpSessionStateWrapper(HttpContext.Current.Session);
        }

        public CacheEnderecosSessaoHttp(HttpSessionStateBase httpSessionStateBase)
        {
            _session = httpSessionStateBase;
        }

        public IList<Cidade> GetCidades()
        {
            object cidade = _session[ChavesSessao.CidadesAtual];
            if (cidade == null)
            {
                cidade = new List<Cidade>();
            }
            return (IList<Cidade>)cidade;
        }

        public IList<Bairro> GetBairros()
        {
            object bairro = _session[ChavesSessao.BairrosAtual];
            if (bairro == null)
            {
                bairro = new List<Bairro>();
            }
            return (IList<Bairro>)bairro;
        }

        public IList<Logradouro> GetLogradouros()
        {
            object logradouro = _session[ChavesSessao.LogradourosAtual];
            if (logradouro == null)
            {
                logradouro = new List<Logradouro>();
            }
            return (IList<Logradouro>)logradouro;
        }

        public void SetCidades(IList<Cidade> cidades)
        {
            _session[ChavesSessao.CidadesAtual] = cidades;
        }

        public void SetBairros(IList<Bairro> bairros)
        {
            _session[ChavesSessao.BairrosAtual] = bairros;
        }

        public void SetLogradouros(IList<Logradouro> logradouros)
        {
            _session[ChavesSessao.LogradourosAtual] = logradouros;
        }

        private HttpSessionStateBase _session;
    }
}