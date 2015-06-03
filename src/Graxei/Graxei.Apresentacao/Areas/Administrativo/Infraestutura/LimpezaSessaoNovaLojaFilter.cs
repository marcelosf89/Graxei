using System.Web.Mvc;

namespace Graxei.Apresentacao.Areas.Administrativo.Infraestutura
{
    public class LimpezaSessaoNovaLojaAttribute : ActionFilterAttribute
    {
        #region Implementation of IActionFilter

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is PartialViewResult)
            {
                PartialViewResult pv = (PartialViewResult) filterContext.Result;
                if (pv.ViewName.ToLower() == "incluida")
                {
                    filterContext.HttpContext.Session[ChavesSessao.EnderecosNovaLoja] = null;
                    filterContext.HttpContext.Session[ChavesSessao.Logotipo] = null;
                }
            }
            
        }

        #endregion
    }
}