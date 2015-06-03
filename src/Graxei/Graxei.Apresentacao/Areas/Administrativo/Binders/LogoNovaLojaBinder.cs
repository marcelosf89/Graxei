using System.Web.Mvc;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.Areas.Administrativo.Models;

namespace Graxei.Apresentacao.Areas.Administrativo.Binders
{
    public class LogoNovaLojaBinder : IModelBinder
    {


        public object BindModel(ControllerContext controllerContext,
                               ModelBindingContext bindingContext)
        {
            if (controllerContext.Controller.ValueProvider.GetValue("controller").AttemptedValue == "Lojas"
                && controllerContext.Controller.ValueProvider.GetValue("action").AttemptedValue == "Index")
            {
                controllerContext.HttpContext.Session[ChavesSessao.Logotipo] = null;
            }
            LogotipoNovaLojaModel l = null;
            if (controllerContext.HttpContext.Session[ChavesSessao.Logotipo] == null)
            {
                l = new LogotipoNovaLojaModel();
                controllerContext.HttpContext.Session[ChavesSessao.Logotipo] = l;
            }else
            {
                l = (LogotipoNovaLojaModel)controllerContext.HttpContext.Session[ChavesSessao.Logotipo];
            }
            return l;
        }
    }
}