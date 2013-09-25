using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Infraestutura;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Areas.Administrativo.Models;
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
        public ActionResult Index(string nomeLoja = "")
        {
            IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            EnderecoNovoModel model = new EnderecoNovoModel() { Endereco = new Endereco(), IdEstado = 0, NomeLoja = nomeLoja};
            return View("Novo", model);
        }

        [HttpPost]
        public RedirectToRouteResult Novo(EnderecoNovoModel item)
        {
            Estado estado = _servicoEnderecos.GetEstado(item.IdEstado);
            item.Endereco.Bairro.Cidade.Estado = estado;
            AdicionarEnderecoModel(item);
            return RedirectToAction("Novo", "Lojas", new { item.NomeLoja });
        }

        public RedirectToRouteResult Excluir(int idLista)
        {
            Enderecos.RemoveAll(p => p.Id == idLista);
            return RedirectToAction("Novo", "Lojas" );
        }

        public ActionResult EstadoSelecionado(string idEstado)
        {
            int id = int.Parse(idEstado);
            Cidades = _servicoEnderecos.GetCidades(id);
            IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return View("Novo");
        }

        public ActionResult CidadeSelecionada(string idEstado, string valCidade)
        {
            int id = int.Parse(idEstado);
            Bairros = _servicoEnderecos.GetBairros(valCidade, id);
            IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return View("Novo");
        }

        public ActionResult AutoCompleteCidade(string term)
        {
            string[] itens = Cidades.Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            /*IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");*/
            return Json(itensFiltrados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoCompleteBairro(string term)
        {
            string[] itens = Bairros.Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            /*IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");*/
            return Json(itensFiltrados, JsonRequestBehavior.AllowGet);
        }

        #region Métodos Privados
        /// <summary>
        /// Adiciona o endereço recém-cadastrado à lista Http Session de endereços
        /// </summary>
        /// <param name="item"></param>
        private void AdicionarEnderecoModel(EnderecoNovoModel item)
        {
            int i = Enderecos.Count();
            ItemListaNovosEnderecosModel novo = new ItemListaNovosEnderecosModel {Id = i, Endereco = item.Endereco};
            Enderecos.Add(novo);
            i = 0;
            foreach (ItemListaNovosEnderecosModel n in Enderecos.OrderBy(p => p.Id))
            {
                n.Id = i++;
            }
        }
        #endregion

        #region Propriedades de Sessão

        private IList<Cidade> Cidades
        {
            get
            {
                object cidade = Session[ItensSessao.CidadesAtual];
                if (cidade == null)
                {
                    cidade = new List<Cidade>();
                }
                return (IList<Cidade>)cidade;
            }
            set
            {
                Session[ItensSessao.CidadesAtual] = value;
            }
        }

        private IList<Bairro> Bairros
        {
            get
            {
                object bairro = Session[ItensSessao.BairrosAtual];
                if (bairro == null)
                {
                    bairro = new List<Bairro>();
                }
                return (IList<Bairro>)bairro;
            }
            set
            {
                Session[ItensSessao.BairrosAtual] = value;
            }
        }


        /* TODO: Refazer os mecanismos de acesso a elementos de sessão Http*/
        private List<ItemListaNovosEnderecosModel> Enderecos
        {
            get
            {
                if (Session[ItensSessao.EnderecosNovaLoja] == null)
                {
                    Session[ItensSessao.EnderecosNovaLoja] = new List<ItemListaNovosEnderecosModel>();
                }
                return (List<ItemListaNovosEnderecosModel>)Session[ItensSessao.EnderecosNovaLoja];
            }
            set
            {
                Session[ItensSessao.EnderecosNovaLoja] = value;
            }
        }
        #endregion

        #region Atributos Privados
        private readonly IServicoEnderecos _servicoEnderecos;
        #endregion
    }
}
