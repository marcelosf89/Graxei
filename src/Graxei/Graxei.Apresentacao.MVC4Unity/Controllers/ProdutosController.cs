using System;
using Graxei.Modelo;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Transversais.Utilidades;
using System.Configuration;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Controllers
{
    public class ProdutosController : Controller
    {

        public ProdutosController(IConsultaFabricantes appConsultasFabricantes)
        {
            _appConsultasFabricantes = appConsultasFabricantes;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Novo()
        {
            return View();
        }

        public ActionResult Copiar()
        {
            return View();
        }

        public ActionResult Autocomplete(string term)
        {
            IList<Fabricante> fabs = null;
            if (Session[Constantes.Fabricantes] == null)
            {
                Session[Constantes.Fabricantes] = _appConsultasFabricantes.TodosNomes(); 
            }
            string[] nomes = ((IList<string>)Session[Constantes.Fabricantes]).ToArray();
            return Json(nomes, JsonRequestBehavior.AllowGet);
        }



        private readonly IConsultaFabricantes _appConsultasFabricantes;

    }
}
