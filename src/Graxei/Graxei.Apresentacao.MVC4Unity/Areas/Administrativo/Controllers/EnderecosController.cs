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
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.Cache;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class EnderecosController : Controller
    {

        public EnderecosController(IConsultaEnderecos consultasEnderecos, IConsultasLojas consultasLojas, IConsultaEstados consultasEstados, IConsultaCidades consultasCidades, IGerenciamentoEnderecos gerenciamentoEnderecos, IOperacaoEndereco operacaoEndereco, ICacheElementosEndereco cacheElementosEndereco)
        {
            _consultaEnderecos = consultasEnderecos;
            _consultasLojas = consultasLojas;
            _consultasEstados = consultasEstados;
            _consultasCidades = consultasCidades;
            _gerenciamentoEnderecos = gerenciamentoEnderecos;
            _operacaoEndereco = operacaoEndereco;
            _cacheElementosEndereco = cacheElementosEndereco;
        }

        public ActionResult Editar(long idEndereco)
        {
            ModelState.Clear();
            EnderecoVistaContrato item;
            Endereco endereco = _consultaEnderecos.Get(idEndereco);
            EnderecosViewModelEntidade transformacao = new EnderecosViewModelEntidade(_operacaoEndereco);
            item = transformacao.Transformar(endereco);

            if (item.IdEstado > 0)
            {
                _cacheElementosEndereco.SetCidades(_consultasCidades.GetPorEstado(item.IdEstado));
            }
                

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
            ViewBag.OperacaoSucesso = Sucesso.EnderecoSalvo;
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
            ViewBag.OperacaoSucesso = Sucesso.EnderecoExcluido;
            return ListaEnderecos(idLoja);
        }

        private readonly IConsultaEnderecos _consultaEnderecos;
        private readonly IConsultasBairros _consultasBairros;
        private readonly IConsultaEstados _consultasEstados;
        private readonly IConsultasLojas _consultasLojas;
        private readonly IConsultaCidades _consultasCidades;
        private readonly IGerenciamentoEnderecos _gerenciamentoEnderecos;
        private readonly IOperacaoEndereco _operacaoEndereco;
        private readonly ICacheElementosEndereco _cacheElementosEndereco;

    }
}
