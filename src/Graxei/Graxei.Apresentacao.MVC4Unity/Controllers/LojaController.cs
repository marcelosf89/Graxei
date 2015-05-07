using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Controllers
{
    public class LojaController : Controller
    {
        public LojaController(IConsultaEnderecos consultasEnderecos, IConsultasLojas consultasLojas,  IConsultasProdutoVendedor consultasProdutoVendedor)
        {
            _consultasEnderecos = consultasEnderecos;
            _consultasLojas = consultasLojas;
            _consultasProdutoVendedor = consultasProdutoVendedor;
        }
        //
        // GET: /Loja/

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult VerLoja(long idEndereco)
        {
            Endereco endereco = _consultasLojas.GetEnderecoComTelefones(idEndereco);
            EnderecoModel em = new EnderecoModel();
            em.Endereco = endereco;
            return View(em);
        }

        [AllowAnonymous]
        public ActionResult IndexPaginaLoja(Loja loja)
        {

            return View(loja);
        }

        public ActionResult GetQuantidadeProdutos(long lojaId)
        {
            long quantidadeProduto = _consultasProdutoVendedor.GetQuantidadeProduto(lojaId);
            return Json(quantidadeProduto, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult GetImagem(int idLoja = 0)
        {
            if (idLoja != 0)
            {
                String caminhoImagem = ConfigurationManager.AppSettings["imagesPath"];
                byte[] file = _consultasLojas.GetImageBackground(idLoja, caminhoImagem);
                if (file != null)
                {
                    return File(file, "image/jpeg");
                }
            }

            return null;
        }

        public FileContentResult GetLogo(int idLoja = 0)
        {
            if (idLoja != 0)
            {
                String caminhoImagem = ConfigurationManager.AppSettings["imagesPath"];
                byte[] file = _consultasLojas.GetLogo(idLoja, caminhoImagem);
                if (file != null)
                {
                    return File(file, "image/jpeg");
                }
            }

            return null;
        }
        
        public ActionResult Produtos(Loja loja)
        {
            return View(loja);
        }

        private IConsultaEnderecos _consultasEnderecos;
        private IConsultasLojas _consultasLojas;
        private IConsultasProdutoVendedor _consultasProdutoVendedor;
    }
}
