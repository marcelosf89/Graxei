using System;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Models;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Controllers
{
    public class ProdutosController : Controller
    {

        public ProdutosController(IServicoProdutos servicoProdutos, IServicoFabricantes servicoFabricantes)
        {
            _servicoProdutos = servicoProdutos;
            _servicoFabricantes = servicoFabricantes;
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
                Session[Constantes.Fabricantes] = _servicoFabricantes.TodosNomes(); 
            }
            fabs = (IList<Fabricante>)Session[Constantes.Fabricantes];
            string[] nomes = fabs.Where(p => p.Nome.IndexOf(term, StringComparison.CurrentCultureIgnoreCase) >= 0).Select(p => p.Nome).ToArray();
            return Json(nomes, JsonRequestBehavior.AllowGet);
        }

        private readonly IServicoProdutos _servicoProdutos;
        private readonly IServicoFabricantes _servicoFabricantes;

    }
}
