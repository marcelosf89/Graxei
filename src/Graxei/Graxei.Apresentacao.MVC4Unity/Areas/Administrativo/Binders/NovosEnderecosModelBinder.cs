using System.Web.Mvc;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Models;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Binders
{
    public class NovosEnderecosModelBinder : IModelBinder
    {
        private string _chaveSessao = "nemb#key";
        public object BindModel(ControllerContext controllerContext,
                               ModelBindingContext bindingContext)
        {
            NovosEnderecosModel e = (NovosEnderecosModel)controllerContext.HttpContext.Session[_chaveSessao];
            if (e == null)
            {
                e = new NovosEnderecosModel();
                controllerContext.HttpContext.Session[_chaveSessao] = e;
            }
            return e;
        }
    }
}