using System.Web.Mvc;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo
{
    public class AdministrativoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrativo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                namespaces: new string[] { "Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Controllers" },
                name: "Administrativo_default",
                url: "Administrativo/{controller}/{action}/{id}",
                 defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
