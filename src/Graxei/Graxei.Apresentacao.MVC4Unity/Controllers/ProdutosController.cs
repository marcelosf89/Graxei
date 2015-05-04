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

        public FileResult GetThumbnail(int idProduto = 0)
        {
            if (idProduto != 0)
            {
                String caminhoImagem = ConfigurationManager.AppSettings["imagesPath"];
                byte[] file = _appConsultasFabricantes.GetThumbnail(idProduto, caminhoImagem);
                if (file != null)
                {
                    return File(file, "image/jpeg");
                }
            }

            return File("PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIxNzEiIGhlaWdodD0iMTgwIj48cmVjdCB3aWR0aD0iMTcxIiBoZWlnaHQ9IjE4MCIgZmlsbD0iI2VlZSIvPjx0ZXh0IHRleHQtYW5jaG9yPSJtaWRkbGUiIHg9Ijg1LjUiIHk9IjkwIiBzdHlsZT0iZmlsbDojYWFhO2ZvbnQtd2VpZ2h0OmJvbGQ7Zm9udC1zaXplOjEycHg7Zm9udC1mYW1pbHk6QXJpYWwsSGVsdmV0aWNhLHNhbnMtc2VyaWY7ZG9taW5hbnQtYmFzZWxpbmU6Y2VudHJhbCI+MTcxeDE4MDwvdGV4dD48L3N2Zz4=", "image/svg+xml");
        }

        private readonly IConsultaFabricantes _appConsultasFabricantes;

    }
}
