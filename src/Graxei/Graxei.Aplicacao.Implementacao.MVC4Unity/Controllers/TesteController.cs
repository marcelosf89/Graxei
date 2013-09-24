using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Controllers
{
    public class TesteController : Controller
    {
        //
        // GET: /Teste/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autocomplete(string term)
        {
            var items = new[] { "Apple", "Pear", "Banana", "Pineapple", "Peach" };

            var filteredItems = items.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

    }
}
