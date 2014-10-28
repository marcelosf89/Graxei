using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Controllers
{
    public class LojaController : Controller
    {
        public LojaController(IConsultasLojas consultasLojas)
        {
            _consultasLojas = consultasLojas;
        }
        //
        // GET: /Loja/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerLoja(long IdTeste)
        {
            Loja loja = _consultasLojas.Get(IdTeste);
            return View(loja);
        }


        private IConsultasLojas _consultasLojas;
    }
}
