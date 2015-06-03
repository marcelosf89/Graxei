using System.Web.Mvc;

namespace Graxei.Apresentacao.Areas.Teste
{
    public class TesteAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Teste";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Teste_default",
                "Teste/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
