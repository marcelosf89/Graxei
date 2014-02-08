using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Modelo;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class EnderecosController : Controller
    {

        public EnderecosController(IConsultasEnderecos consultasEnderecos)
        {
            _consultaEnderecos = consultasEnderecos;
        }

        public ActionResult Index(EnderecosModel item)
        {
            IList<Estado> estados = _consultaEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            EnderecoIndiceModel model = new EnderecoIndiceModel() {IdEstado = estados[0].Id};
            return PartialView("Novo", model);
        }

        [HttpPost]
        public ActionResult Novo(EnderecosModel item, EnderecoIndiceModel model)
        {
            Estado estado = _consultaEnderecos.GetEstado(model.IdEstado);
            model.Endereco.Bairro.Cidade.Estado = estado;
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }
            item.AdicionarEndereco(model);
            IList<Endereco> enderecos = item.Enderecos.Select(e => e.Endereco).ToList();
            if (_consultaEnderecos.EnderecosRepetidos(enderecos).Any())
            {
                ModelState.AddModelError(String.Empty, Erros.EnderecoRepetidoLoja);
                return PartialView(model);
            }
            HttpContext.Session[ChavesSessao.EnderecosNovaLoja] = null;
            HttpContext.Session[ChavesSessao.Logotipo] = null;
            return PartialView("AcoesEmLoja", item);
        }

        public ActionResult Novo(EnderecoIndiceModel model)
        {
            IList<Estado> estados = _consultaEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return View();
        }

        public ActionResult Editar(EnderecosModel item, int id)
        {
            EnderecoIndiceModel endereco = item.Enderecos.SingleOrDefault(p => p.IdLista == id);
            endereco.IdEstado = (int)endereco.Endereco.Bairro.Cidade.Estado.Id;
            IList<Estado> estados = _consultaEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return View("Editar", endereco);
        }

        [HttpPost]
        public ActionResult Editar(EnderecosModel item, EnderecoIndiceModel model)
        {
            item.SubstituirEndereco(model);
            Estado estado = _consultaEnderecos.GetEstado(model.IdEstado);
            model.Endereco.Bairro.Cidade.Estado = estado;
            return PartialView("AcoesEmLoja", item);
        }

        public ActionResult Excluir(EnderecosModel item, int id)
        {
            item.RemoverEndereco(id);
            return PartialView("AcoesEmLoja", item);
        }

        public ActionResult NovoTelefone(EnderecoIndiceModel model)
        {
            return View("Novo", model);
        }

        #region AutoComplete
        public ActionResult EstadoSelecionado(string idEstado)
        {
            int id = int.Parse(idEstado);
            Cidades = _consultaEnderecos.GetCidades(id);
            IList<Estado> estados = _consultaEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return View("Formulario");
        }

        public ActionResult CidadeSelecionada(string idEstado, string valCidade)
        {
            int id = int.Parse(idEstado);
            Bairros = _consultaEnderecos.GetBairros(valCidade, id);
            IList<Estado> estados = _consultaEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return View("Formulario");
        }

        public ActionResult BairroSelecionado(long idEstado, string valCidade, string valBairro)
        {
            Logradouros = _consultaEnderecos.GetLogradouros(valBairro, valCidade, idEstado);
            return View("Formulario");
        }

        public ActionResult AutoCompleteCidade(string term)
        {
            string[] itens = Cidades.Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            /*IList<Estado> estados = _consultaEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");*/
            return Json(itensFiltrados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoCompleteBairro(string term)
        {
            string[] itens = Bairros.Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            /*IList<Estado> estados = _consultaEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");*/
            return Json(itensFiltrados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoCompleteLogradouro(string term)
        {
            string[] itens = Logradouros.Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            /*IList<Estado> estados = _consultaEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");*/
            return Json(itensFiltrados, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Propriedades de Sessão
        /* TODO: Refazer os mecanismos de acesso a elementos de sessão Http*/
        private IList<Cidade> Cidades
        {
            get
            {
                object cidade = Session[ChavesSessao.CidadesAtual];
                if (cidade == null)
                {
                    cidade = new List<Cidade>();
                }
                return (IList<Cidade>)cidade;
            }
            set
            {
                Session[ChavesSessao.CidadesAtual] = value;
            }
        }

        private IList<Bairro> Bairros
        {
            get
            {
                object bairro = Session[ChavesSessao.BairrosAtual];
                if (bairro == null)
                {
                    bairro = new List<Bairro>();
                }
                return (IList<Bairro>)bairro;
            }
            set
            {
                Session[ChavesSessao.BairrosAtual] = value;
            }
        }

        private IList<Logradouro> Logradouros
        {
            get
            {
                object logradouro = Session[ChavesSessao.LogradourosAtual];
                if (logradouro == null)
                {
                    logradouro = new List<Logradouro>();
                }
                return (IList<Logradouro>)logradouro;
            }
            set
            {
                Session[ChavesSessao.LogradourosAtual] = value;
            }
        }

        #endregion

        #region Atributos Privados
        private readonly IConsultasEnderecos _consultaEnderecos;
        #endregion

    }
}
