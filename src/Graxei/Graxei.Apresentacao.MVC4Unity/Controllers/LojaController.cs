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
        public LojaController(IConsultasEnderecos consultasEnderecos)
        {
            _consultasEnderecos = consultasEnderecos;
        }
        //
        // GET: /Loja/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerLoja(long IdTeste)
        {
            Endereco endereco = _consultasEnderecos.Get(IdTeste);
            return View(endereco);
        }


        private IConsultasEnderecos _consultasEnderecos;
    }
}
