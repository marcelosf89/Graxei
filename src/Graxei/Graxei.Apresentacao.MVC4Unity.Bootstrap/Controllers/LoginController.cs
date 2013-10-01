using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View("Autenticacao");
        }


        public ActionResult Autenticacao()
        {
            return View(new Modelo.Loja());
        }

        [HttpPost]
        public ActionResult Autenticacao(Graxei.Modelo.Loja loja)
        {
            return Redirect("~/Administrativo/Home");
        }

        public ActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public void RedefinirSenha(object obj)
        {
        }
        

    }
}
