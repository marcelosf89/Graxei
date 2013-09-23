using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Models;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class EnderecosController : Controller
    {

        public EnderecosController(IServicoEnderecos servicoEnderecos)
        {
            _servicoEnderecos = servicoEnderecos;
        }

        //
        // GET: /Administrativo/Enderecos/

        public ActionResult Index()
        {
            IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.IdEstado = new SelectList(estados, "Id", "Sigla");
            return View("Novo");
        }

        [HttpPost]
        public RedirectToRouteResult Novo(LojaEnderecoViewModel item)
        {
            Estado estado = _servicoEnderecos.GetEstado(item.IdEstado);
            item.Endereco.Bairro.Cidade.Estado = estado;
            Enderecos.Add(item.Endereco);
            return RedirectToAction("Novo", "Lojas", new { item.NomeLoja });
        }

        public ActionResult EstadoSelecionado(string idEstado)
        {
            int id = int.Parse(idEstado);
            Cidades = _servicoEnderecos.GetCidades(id);
            return View("Novo");
        }

        public ActionResult AutoCompleteCidade(string term)
        {
            string[] items = Cidades.Select(p => p.Nome).ToArray();
            var filteredItems = items.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoCompleteBairro(string term)
        {
            IList<Cidade> filteredItems =
                Cidades.Where(p => p.Nome.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) > 0).ToList<Cidade>();
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        #region Propriedades de Sessão

        private IList<Cidade> Cidades
        {
            get
            {
                object cidade = Session["CidadeAtual"];
                if (cidade == null)
                {
                    cidade = new List<Cidade>();
                }
                return (IList<Cidade>)cidade;
            }
            set
            {
                Session["CidadeAtual"] = value;
            }
        }

        private IList<Endereco> Enderecos
        {
            get
            {
                if (Session["EnderecosNovaLoja"] == null)
                {
                    Session["EnderecosNovaLoja"] = new List<Endereco>();
                }
                return (IList<Endereco>)Session["EnderecosNovaLoja"];
            }
            set
            {
                Session["EnderecosNovaLoja"] = value;
            }
        }
        #endregion

        #region Atributos Privados
        private readonly IServicoEnderecos _servicoEnderecos;
        #endregion
    }
}
