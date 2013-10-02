using System;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Graxei.Apresentacao.MVC4Unity.Models;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
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
            string[] nomes = ((IList<string>)Session[Constantes.Fabricantes]).ToArray();
            return Json(nomes, JsonRequestBehavior.AllowGet);
        }

        private readonly IServicoProdutos _servicoProdutos;
        private readonly IServicoFabricantes _servicoFabricantes;

    }
}