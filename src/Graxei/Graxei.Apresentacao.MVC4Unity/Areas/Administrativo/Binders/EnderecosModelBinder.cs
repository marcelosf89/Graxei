using System.Web.Mvc;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Binders
{
    public class EnderecosModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
                               ModelBindingContext bindingContext)
        {
            if (controllerContext.Controller.ValueProvider.GetValue("controller").AttemptedValue == "Lojas"
                && controllerContext.Controller.ValueProvider.GetValue("action").AttemptedValue == "Index")
            {
                controllerContext.HttpContext.Session[ChavesSessao.EnderecosNovaLoja] = null;
            }
            EnderecosModel e = null;
            if (controllerContext.HttpContext.Session[ChavesSessao.EnderecosNovaLoja] == null)
            {
                e = new EnderecosModel();
                controllerContext.HttpContext.Session[ChavesSessao.EnderecosNovaLoja] = e;
            }else
            {
                e = (EnderecosModel)controllerContext.HttpContext.Session[ChavesSessao.EnderecosNovaLoja];
            }
            return e;
        }
    }
}