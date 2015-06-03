using System.Web.Mvc;
using Graxei.Apresentacao.Models;
using Graxei.Modelo;
using Graxei.Transversais.Comum;

namespace Graxei.Apresentacao.Binders
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