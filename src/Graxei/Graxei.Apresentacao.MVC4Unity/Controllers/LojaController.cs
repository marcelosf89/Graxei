using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Controllers
{
    public class LojaController : Controller
    {
        //
        // GET: /Loja/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerLoja(int IdTeste)
        {
            return View();
        }

    }
}
