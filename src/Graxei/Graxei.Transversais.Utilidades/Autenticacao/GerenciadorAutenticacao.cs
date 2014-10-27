using System;
using System.Web;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;

namespace Graxei.Transversais.Utilidades.Autenticacao
{
    public class GerenciadorAutenticacaoSessaoHttp : IGerenciadorAutenticacao
    {

        public void Registrar(Usuario usuario)
        {
            HttpContext.Current.Session["UsuarioAtual"] = usuario;
        }

        public Usuario Get()
        {
            if (HttpContext.Current.Session["UsuarioAtual"] != null)
            {
                return (Usuario) HttpContext.Current.Session["UsuarioAtual"];
            }
            return null;
        }
    }
}