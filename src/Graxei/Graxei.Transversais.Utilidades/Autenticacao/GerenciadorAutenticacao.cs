using System.Web;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;

namespace Graxei.Transversais.Utilidades.Autenticacao
{
    public class GerenciadorAutenticacaoSessaoHttp : IGerenciadorAutenticacao
    {

        public void Registrar(Usuario usuario)
        {
            HttpContext.Current.Session[Constantes.UsuarioAtual] = usuario;
        }

        public Usuario Get()
        {
            if (HttpContext.Current.Session[Constantes.UsuarioAtual] != null)
            {
                return (Usuario)HttpContext.Current.Session[Constantes.UsuarioAtual];
            }
            return null;
        }
    }
}