using System.Web;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;

namespace Graxei.Apresentacao.MVC4Unity
{
    public class Helper
    {
        internal static void SetUsuarioLogado(HttpSessionStateBase session, Usuario usuarioAutenticado)
        {
            UsuarioLogado logado = new UsuarioLogado() {Usuario = usuarioAutenticado};
            session[Constantes.UsuarioAtual] = logado;
        }
    }
}