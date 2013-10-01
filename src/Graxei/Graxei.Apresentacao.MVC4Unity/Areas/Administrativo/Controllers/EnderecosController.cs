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
        public ActionResult Index(NovosEnderecosModel item)
        {
            IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            //EnderecoIndiceModel model = new EnderecoIndiceModel(){ Endereco = new Endereco()};
            return View("Novo");
        }

  
        //public RedirectToRouteResult Novo(NovaLojaEnderecosModel item, EnderecoIndiceModel endereco)
        [HttpPost]
        public RedirectToRouteResult Novo(NovosEnderecosModel item, EnderecoIndiceModel model)
        {
            Estado estado = _servicoEnderecos.GetEstado(model.IdEstado);
            model.Endereco.Bairro.Cidade.Estado = estado;
            item.AdicionarEndereco(model);
            return RedirectToAction("Index", "Lojas");
        }

        public ActionResult Editar(NovaLojaModel item, int id)
        {
            EnderecoIndiceModel endereco = item.NovosEnderecosModel.Enderecos.SingleOrDefault(p => p.IdLista == id);
            endereco.IdEstado = (int)endereco.Endereco.Bairro.Cidade.Estado.Id;
            IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.NomeLoja = item.Loja.Nome;
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return View("Editar", endereco);
        }

        [HttpPost]
        public RedirectToRouteResult Editar(NovaLojaModel item, EnderecoIndiceModel model)
        {
            Estado estado = _servicoEnderecos.GetEstado(model.IdEstado);
            model.Endereco.Bairro.Cidade.Estado = estado;
            return RedirectToAction("Novo", "Lojas");
        }

        public RedirectToRouteResult Excluir(NovaLojaModel item, int id)
        {
            item.NovosEnderecosModel.RemoverEndereco(id);
            return RedirectToAction("Novo", "Lojas" );
        }

        public ActionResult EstadoSelecionado(string idEstado)
        {
            int id = int.Parse(idEstado);
            Cidades = _servicoEnderecos.GetCidades(id);
            IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return View("Formulario");
        }

        public ActionResult CidadeSelecionada(string idEstado, string valCidade)
        {
            int id = int.Parse(idEstado);
            Bairros = _servicoEnderecos.GetBairros(valCidade, id);
            IList<Estado> estados = _servicoEnderecos.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return View("Formulario");
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
       /* private void AdicionarEnderecoModel(EnderecosNovaLoja item)
        {
            int i = Enderecos.Count();
            EnderecosNovaLoja novo = new EnderecosNovaLoja { IdLista = i, Endereco = item.Endereco };
            Enderecos.Add(novo);
            i = 0;
            foreach (EnderecosNovaLoja n in Enderecos.OrderBy(p => p.IdLista))
            {
                n.IdLista = i++;
            }
        }*/

        /// <summary>
        /// Edita o endereço cadastrado na lista Http Session de endereços
        /// </summary>
        /// <param name="item"></param>
        /// <param name="idLista"> </param>
        /*private void EditarEnderecoModel(EnderecosNovaLoja item, int idLista)
        {
            EnderecosNovaLoja end = Enderecos.SingleOrDefault(p => p.IdLista == idLista);
            end.Endereco = item.Endereco;
        }*/
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
        /*private List<EnderecosNovaLoja> Enderecos
        {
            get
            {
                if (Session[ItensSessao.EnderecosNovaLoja] == null)
                {
                    Session[ItensSessao.EnderecosNovaLoja] = new List<EnderecosNovaLoja>();
                }
                return (List<EnderecosNovaLoja>)Session[ItensSessao.EnderecosNovaLoja];
            }
            set
            {
                Session[ItensSessao.EnderecosNovaLoja] = value;
            }
        }*/
        #endregion

        #region Atributos Privados
        private readonly IServicoEnderecos _servicoEnderecos;
        #endregion
    }
}
