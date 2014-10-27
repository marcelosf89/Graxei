using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Controllers
{
    public class HomeController : Controller
    {
        private DateTime date;

        public HomeController(IConsultasProdutoVendedor consultaVendedor)
        {
            _iConsultasProdutoVendedor = consultaVendedor;
            date = DateTime.Now;
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(date);
        }

        public ActionResult Pesquisar(string txtSearch)
        {
            IList<ProdutoVendedor> list = _iConsultasProdutoVendedor.Get(txtSearch);
            return View(list);
        }

        public ActionResult VerLoja()
        {
            return RedirectToAction("VerLoja", "Loja", new { id = 0 });
        }

        IConsultasProdutoVendedor _iConsultasProdutoVendedor;
    }
}
