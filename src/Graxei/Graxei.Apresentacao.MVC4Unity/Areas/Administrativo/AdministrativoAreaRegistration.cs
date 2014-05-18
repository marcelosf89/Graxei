using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo
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
                namespaces: new string[] { "Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers" },
                name: "Administrativo_default",
                url: "Administrativo/{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
