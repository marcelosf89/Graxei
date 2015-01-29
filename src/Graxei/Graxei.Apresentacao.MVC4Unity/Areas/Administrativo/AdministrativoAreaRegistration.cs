using System.Web.Mvc;
using System.Web.Optimization;

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

            context.MapRoute(
                    "ListarLojas",
                    "Administrativo/Lojas/Listar/{numeroPagina}/{tamanho}",
                    new {controller = "Lojas", Action = "Listar", numeroPagina = UrlParameter.Optional, tamanho = UrlParameter.Optional});

            RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Administrativo/Produtos/Pesquisar/js").Include("~/Script/Administrativo/Produtos/pesquisar.js"));
        }
    }
}
