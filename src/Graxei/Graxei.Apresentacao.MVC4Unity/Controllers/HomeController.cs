using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            IList<ProdutoVendedor> list;
            IpRegiaoModel ir = (IpRegiaoModel)Session["IpRegiaoModel"];
            if (ir == null)
                list = _iConsultasProdutoVendedor.Get(txtSearch, "", "");
            else
                list = _iConsultasProdutoVendedor.Get(txtSearch, ir.Pais, ir.Cidade);

            return View(list);
        }

        [HttpPost]
        public void SetRegionIP(string pais, string cidade, string regiao)
        {
            IpRegiaoModel ir = new IpRegiaoModel()
            {
                Cidade = cidade,
                Pais = pais,
                EstadoCodigo = Convert.ToInt32(regiao)
            };
            Session["IpRegiaoModel"] = ir;
            return;
        }

        public ActionResult VerLoja()
        {
            return RedirectToAction("VerLoja", "Loja", new { id = 0 });
        }

        [HttpPost]
        public void SetFiltro(string filtro, string valor)
        {

        }


        IConsultasProdutoVendedor _iConsultasProdutoVendedor;
    }
}
