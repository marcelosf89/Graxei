using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Fabrica;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Apresentacao.MVC4Unity.Infrastructure;
using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.Entidades;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Idiomas;
using Graxei.Aplicacao.Contrato.Operacoes;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class EnderecosController : Controller
    {

        public EnderecosController(IConsultaEnderecos consultasEnderecos, IConsultasBairros consultasBairros, IConsultasLojas consultasLojas, IConsultaEstados consultasEstados, IConsultaCidades consultasCidades, IConsultasLogradouros consultasLogradouros, IGerenciamentoEnderecos gerenciamentoEnderecos, IConsultasTiposTelefone consultasTiposTelefone, IOperacaoEndereco operacaoEndereco)
        {
            _consultaEnderecos = consultasEnderecos;
            _consultasBairros = consultasBairros;
            _consultasLojas = consultasLojas;
            _consultasEstados = consultasEstados;
            _consultasCidades = consultasCidades;
            _consultasLogradouros = consultasLogradouros;
            _gerenciamentoEnderecos = gerenciamentoEnderecos;
            _consultasTiposTelefone = consultasTiposTelefone;
            _operacaoEndereco = operacaoEndereco;
        }

        public ActionResult EditarEndereco(long idEndereco)
        {
            ModelState.Clear();
            EnderecoVistaContrato item;
            Endereco endereco = _consultaEnderecos.Get(idEndereco);
            EnderecosViewModelEntidade transformacao = new EnderecosViewModelEntidade(_operacaoEndereco);
            item = transformacao.Transformar(endereco);

            if (item.IdEstado > 0)
                Cidades = _consultasCidades.GetPorEstado(item.IdEstado);

            IList<Estado> estados = _consultasEstados.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return PartialView("ModalEndereco", item);
        }

        public ActionResult NovoEndereco(long idLoja)
        {
            ModelState.Clear();
            EnderecoVistaContrato item = new EnderecoVistaContrato();
            item.IdLoja = idLoja;

            IList<Estado> estados = _consultasEstados.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return PartialView("ModalEndereco", item);
        }

        [HttpPost]
        public ActionResult NovoEnderecoRetorno(EnderecoVistaContrato endereco)
        {
            IList<Estado> estados = _consultasEstados.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return PartialView("ModalEndereco", endereco);
        }

        public ActionResult ListaEnderecos(long idLoja)
        {
            try
            {
                Loja loja = _consultasLojas.Get(idLoja);
                if (loja == null)
                {
                    throw new OperacaoEntidadeException(string.Format("Loja com id {0} não pôde ser encontrada",
                        idLoja));
                }
                ListaEnderecoModel em = new ListaEnderecoModel();
                List<Endereco> enderecos = _consultaEnderecos.GetPorLoja(loja.Id);
                em.IdLoja = loja.Id;
                em.QuantidadeEndereco = loja.Plano.QuantidadeFilial;
                em.Enderecos = new List<EnderecoListaContrato>();
                foreach (Endereco end in enderecos)
                {
                    em.Enderecos.Add(new EnderecoListaContrato(end.Id, end.ToString(), end.Cnpj));
                }
                return PartialView("ListaEnderecos", em);
            }
            catch (GraxeiException graxeiException)
            {
                return Json(new { Mensagem = graxeiException.Message });
            }
        }

        [HttpGet]
        public ActionResult Get(long idEndereco)
        {
            Endereco endereco = _consultaEnderecos.Get(idEndereco);
            EnderecosViewModelEntidade transformacao = new EnderecosViewModelEntidade(_operacaoEndereco);
            EnderecoVistaContrato item = transformacao.Transformar(endereco);
            return PartialFormularioEndereco(item);
        }

        private ActionResult PartialFormularioEndereco(EnderecoVistaContrato item)
        {
            IList<Estado> estados = _consultasEstados.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return PartialView("FormularioEndereco", item);
        }

        [HttpPost]
        public ActionResult Salvar(EnderecoVistaContrato enderecoModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialFormularioEndereco(enderecoModel);
            }
            try
            {
                _gerenciamentoEnderecos.Salvar(enderecoModel);
            }
            catch (GraxeiException graxeiException)
            {
                ModelState.AddModelError(string.Empty, graxeiException.Message);
                return PartialFormularioEndereco(enderecoModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o endereço. Por favor, contate-nos");
                return PartialFormularioEndereco(enderecoModel);
            }
            ModelState.Clear();
            ViewBag.OperacaoSucesso = Sucesso.EmailEnviado;
            return PartialFormularioEndereco(enderecoModel);

        }

        [HttpPost]
        public ActionResult ExcluirEndereco(long idEndereco, long idLoja)
        {
            try
            {
                throw new Exception();
                Endereco endereco = _consultaEnderecos.Get(idEndereco);
                _gerenciamentoEnderecos.Remover(endereco);
            }
            catch (GraxeiException graxeiException)
            {
                ModelState.AddModelError(string.Empty, graxeiException.Message);
                return ListaEnderecos(idLoja);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o endereço. Por favor, contate-nos");
                return ListaEnderecos(idLoja);
            }
            ModelState.Clear();
            ViewBag.OperacaoSucesso = Sucesso.EmailEnviado;
            return ListaEnderecos(idLoja);
        }


        #region AutoComplete

        public ActionResult EstadoSelecionado(string idEstado)
        {
            int id = int.Parse(idEstado);
            Cidades = _consultasCidades.GetPorEstado(id);
            return null;
        }

        public ActionResult CidadeSelecionada(string idEstado, string cidade)
        {
            int id = int.Parse(idEstado);
            Bairros = _consultasBairros.GetPorCidade(cidade, id);
            return null;
        }

        public ActionResult BairroSelecionado(long estado, string cidade, string bairro)
        {
            Logradouros = _consultasLogradouros.Get(bairro, cidade, estado);
            return null;
        }

        public ActionResult AutoCompleteCidade(string term)
        {
            string[] itens = Cidades.Select(p => p.Nome).ToArray();
            IEnumerable<String> itensFiltrados = itens.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
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

        private readonly IConsultaEnderecos _consultaEnderecos;
        private readonly IConsultasLogradouros _consultasLogradouros;
        private readonly IConsultasBairros _consultasBairros;
        private readonly IConsultaEstados _consultasEstados;
        private readonly IConsultasLojas _consultasLojas;
        private readonly IConsultaCidades _consultasCidades;
        private readonly IConsultasTiposTelefone _consultasTiposTelefone;
        private readonly IGerenciamentoEnderecos _gerenciamentoEnderecos;
        private readonly IOperacaoEndereco _operacaoEndereco;

    }
}
