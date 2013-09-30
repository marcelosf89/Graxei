using System.Web;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Models;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity
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