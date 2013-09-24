using System.Web;
using System.Web.Mvc;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}