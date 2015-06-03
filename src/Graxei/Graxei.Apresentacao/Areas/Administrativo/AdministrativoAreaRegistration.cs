using System.Web.Mvc;
using System.Web.Optimization;

namespace Graxei.Apresentacao.Areas.Administrativo
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
                namespaces: new string[] { "Graxei.Apresentacao.Areas.Administrativo.Controllers" },
                name: "Administrativo_default",
                url: "Administrativo/{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                    "ListarLojas",
                    "Administrativo/Lojas/Listar/{numeroPagina}/{tamanho}",
                    new { controller = "Lojas", Action = "Listar", numeroPagina = UrlParameter.Optional, tamanho = UrlParameter.Optional });

        }

    }
}
