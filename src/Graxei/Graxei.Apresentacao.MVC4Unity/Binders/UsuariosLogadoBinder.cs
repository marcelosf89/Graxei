using System.Web.Mvc;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;

namespace Graxei.Apresentacao.MVC4Unity.Binders
{
    public class UsuariosLogadoBinder : IModelBinder
    {
        #region Implementation of IModelBinder

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            /* TODO: Checar se o fato de o usuário não existir na sessão deve ser aceito*/
            Usuario usuario = (Usuario)controllerContext.HttpContext.Session[Constantes.UsuarioAtual];
            if (usuario == null)
            {
                usuario = new Usuario();
                controllerContext.HttpContext.Session[Constantes.UsuarioAtual] = usuario;
            }
            return usuario;
       }

        #endregion
    }
}