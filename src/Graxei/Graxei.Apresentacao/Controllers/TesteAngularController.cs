using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.Models;
using Graxei.Modelo;
using Graxei.Transversais.Comum.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Controllers
{
    [AllowAnonymous]
    public class TesteAngularController : Controller
    {
        public TesteAngularController(IConsultaEstados consultaEstados)
        {
            _consultaEstados = consultaEstados;
        }

        // GET: Teste
        public ActionResult ModalEnderecoAngular()
        {
            IList<SelectListItem> listaEstados = _consultaEstados.GetEstados(EstadoOrdem.Sigla).Select(p => new SelectListItem{Text= p.Sigla, Value = p.Id.ToString()}).ToList();
            EnderecoVistaModel novoEnderecoModel = new EnderecoVistaModel { IdLoja = 1, Estados = listaEstados };
            return View(viewName: "ModalEnderecoAngular", model: novoEnderecoModel);
        }

        private IConsultaEstados _consultaEstados;
    }
}