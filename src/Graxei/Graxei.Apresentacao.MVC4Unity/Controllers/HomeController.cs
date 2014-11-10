using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Excecoes;
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
        public HomeController(IConsultasProdutoVendedor consultaVendedor)
        {
            _iConsultasProdutoVendedor = consultaVendedor;
        }
        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Index(string txtSearch)
        {
            return View(txtSearch);
        }

        [AllowAnonymous]
        public ActionResult Pesquisar(string txtSearch)
        {
            IList<ProdutoVendedor> list;
            IpRegiaoModel ir = (IpRegiaoModel)Session["IpRegiaoModel"];
            if (ir == null)
                list = _iConsultasProdutoVendedor.Get(txtSearch, "", "", 0);
            else
                list = _iConsultasProdutoVendedor.Get(txtSearch, ir.Pais, ir.Cidade, 0);

            PesquisarModel pm = new PesquisarModel();
            pm.Texto = txtSearch;
            ViewBag.Page = pm.PaginaSelecionada = 0;
            ViewBag.MaxPage = pm.NumeroMaximoPagina = list.Count < 10 ? 0 : 10;
            TempData["txtSearch"] = pm;

            return View(list);
        }

        public ActionResult PesquisarPagina(string page)
        {
            PesquisarModel pm = (PesquisarModel)TempData["txtSearch"];

            if (page.Equals("p"))
                pm.PaginaSelecionada++;
            else if (page.Equals("a") && pm.PaginaSelecionada > 0)
                pm.PaginaSelecionada--;
            else
                pm.PaginaSelecionada = Convert.ToInt32(page);

            long maxView = ViewBag.MaxPage = pm.PaginaSelecionada + 5;
            if (pm.NumeroMaximoPagina.HasValue)
                maxView = pm.NumeroMaximoPagina.Value;
            else if (pm.PaginaSelecionada < 5)
                maxView = maxView + (5 - pm.PaginaSelecionada);

            IList<ProdutoVendedor> list;
            try
            {
                IpRegiaoModel ir = (IpRegiaoModel)Session["IpRegiaoModel"];
                if (ir == null)
                    list = _iConsultasProdutoVendedor.Get(pm.Texto, "", "", Convert.ToInt32(pm.PaginaSelecionada));
                else
                    list = _iConsultasProdutoVendedor.Get(pm.Texto, ir.Pais, ir.Cidade, Convert.ToInt32(pm.PaginaSelecionada));

                if (list.Count < 10)
                    pm.NumeroMaximoPagina = maxView = pm.PaginaSelecionada;
            }
            catch (ForaDoLimiteException fl)
            {
                list = fl.List;
                pm.NumeroMaximoPagina = pm.PaginaSelecionada = maxView = fl.Max;
            }

            ViewBag.MaxPage = maxView + 1;
            ViewBag.Page = pm.PaginaSelecionada;
            TempData["txtSearch"] = pm;

            return View("Pesquisar", list);
        }

        [HttpPost]
        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult VerLoja()
        {
            return RedirectToAction("VerLoja", "Loja", new { id = 0 });
        }

        [HttpPost]
        [AllowAnonymous]
        public void SetFiltro(string filtro, string valor)
        {

        }


        IConsultasProdutoVendedor _iConsultasProdutoVendedor;
    }
}
