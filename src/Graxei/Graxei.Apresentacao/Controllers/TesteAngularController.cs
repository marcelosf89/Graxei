using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.Infrastructure.ActionResults;
using Graxei.Apresentacao.Models;
using Graxei.Modelo;
using Graxei.Transversais.Comum.Entidades;
using Graxei.Transversais.ContratosDeDados;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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


        public ActionResult GetJson()
        {
            System.Threading.Thread.Sleep(3000);
            //EnderecoInterno json = new EnderecoInterno { Id = 1, Cidade = "Gen Pedreira" };
            EnderecoVistaContrato json = new EnderecoVistaContrato { Id = 1, Cidade = "Gen Pedreira" };
            return new JsonNetResult(json, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            
        }

        public ActionResult TesteJson()
        {
            return View();
        }

        private IConsultaEstados _consultaEstados;

        private class EnderecoInterno
        {
            public long Id { get; set; }
            public string Cidade { get; set; }
        }
    }
}