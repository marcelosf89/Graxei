using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Fabrica;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.Areas.Administrativo.Models;
using Graxei.Apresentacao.Infrastructure;
using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Comum.Entidades;
using Graxei.Transversais.Comum.Excecoes;
using Graxei.Transversais.Idiomas;
using Graxei.Aplicacao.Contrato.Operacoes;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura.Cache;
using Graxei.Transversais.Comum.TransformacaoDados.Interface;
using Microsoft.Practices.Unity;
using Graxei.Apresentacao.Infrastructure.ActionResults;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Graxei.Apresentacao.Models;

namespace Graxei.Apresentacao.Areas.Administrativo.Controllers
{
    public class EnderecosController : Controller
    {

        public EnderecosController(IConsultaEnderecos consultasEnderecos, IConsultasLojas consultasLojas, IConsultaEstados consultasEstados,
                                   IConsultaCidades consultasCidades, IGerenciamentoEnderecos gerenciamentoEnderecos, IOperacaoEndereco operacaoEndereco, ICacheElementosEndereco cacheElementosEndereco,
                                   ITransformacaoMutua<Endereco, EnderecoVistaContrato> transformacaoEndereco)
        {
            _consultaEnderecos = consultasEnderecos;
            _consultasLojas = consultasLojas;
            _consultasEstados = consultasEstados;
            _consultasCidades = consultasCidades;
            _gerenciamentoEnderecos = gerenciamentoEnderecos;
            _operacaoEndereco = operacaoEndereco;
            _cacheElementosEndereco = cacheElementosEndereco;
            _transformacaoEndereco = transformacaoEndereco;
        }

        public JsonNetResult Get(long idEndereco)
        {
            ModelState.Clear();
            Endereco endereco = _consultaEnderecos.Get(idEndereco);
            EnderecoVistaContrato item = _transformacaoEndereco.Transformar(endereco);

            if (item.IdEstado > 0)
            {
                _cacheElementosEndereco.SetCidades(_consultasCidades.GetPorEstado(item.IdEstado));
            }

            return new JsonNetResult(item, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
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
                Plano plano = _consultasLojas.GetPlano(idLoja);
                if (plano == null)
                {
                    throw new OperacaoEntidadeException("Loja com não possui planos");
                }
                List<Endereco> enderecos = _consultaEnderecos.GetPorLoja(idLoja);
                ListaEnderecoModel em = new ListaEnderecoModel(idLoja, plano, enderecos);
                IList<SelectListItem> listaEstados = _consultasEstados.GetEstados(EstadoOrdem.Sigla).Select(p => new SelectListItem { Text = p.Sigla, Value = p.Id.ToString() }).ToList();
                EnderecoVistaModel novoEnderecoModel = new EnderecoVistaModel { IdLoja = 1, Estados = listaEstados };
                em.NovoEnderecoModel = novoEnderecoModel;
                return PartialView("ListaEnderecos", em);
            }
            catch (GraxeiException graxeiException)
            {
                return Json(new { Mensagem = graxeiException.Message });
            }
        }

        [HttpGet]
        public ActionResult Novo(long idLoja)
        {
            ModelState.Clear();
            IList<SelectListItem> listaEstados = _consultasEstados.GetEstados(EstadoOrdem.Sigla).Select(p => new SelectListItem { Text = p.Sigla, Value = p.Id.ToString() }).ToList();
            EnderecoVistaModel enderecoVistaModel = new EnderecoVistaModel { IdLoja = idLoja, Estados = listaEstados };
            return View(viewName: "ModalEnderecoAngular", model: enderecoVistaModel);
        }

        [HttpGet]
        public ActionResult Editar(string idLoja, string idEndereco)
        {
            IList<SelectListItem> listaEstados = _consultasEstados.GetEstados(EstadoOrdem.Sigla).Select(p => new SelectListItem { Text = p.Sigla, Value = p.Id.ToString() }).ToList();
            EnderecoVistaModel enderecoVistaModel = new EnderecoVistaModel{ IdLoja = Int32.Parse(idLoja), IdEndereco = Int32.Parse(idEndereco), Estados = listaEstados };
            return View(viewName: "ModalEnderecoAngular", model: enderecoVistaModel);
        }
        
        //[HttpGet]
        //public ActionResult Get(long idEndereco)
        //{
        //    Endereco endereco = _consultaEnderecos.Get(idEndereco);
        //    EnderecosViewModelEntidade transformacao = new EnderecosViewModelEntidade(_operacaoEndereco);
        //    EnderecoVistaContrato item = transformacao.Transformar(endereco);
        //    return PartialFormularioEndereco(item);
        //}

        private ActionResult PartialFormularioEndereco(EnderecoVistaContrato item)
        {
            IList<Estado> estados = _consultasEstados.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return PartialView("FormularioEndereco", item);
        }

        [HttpPost]
        public JsonNetResult Salvar(EnderecoVistaContrato enderecoModel)
        {
            StatusOperacao statusOperacao = new StatusOperacao();
            statusOperacao.Ok = false;
            
            if (!ModelState.IsValid)
            {
                IList<string> erros = (from modelState in ModelState.Values
                                          where modelState.Errors != null && modelState.Errors.Count > 0
                                          select modelState.Errors)
                                                        .SelectMany(item => item)
                                                        .Select(modelError => modelError.ErrorMessage)
                                                        .ToList();
                statusOperacao.ErrosValidacao = new string[erros.Count];
                for (int i = 0; i < erros.Count; i++)
                {
                    statusOperacao.ErrosValidacao[i] = erros[i];
                }
                statusOperacao.Mensagem = "Modelstate inválido";
            }
            else
            {
                try
                {
                    _gerenciamentoEnderecos.Salvar(enderecoModel);
                    statusOperacao.Ok = true;
                    statusOperacao.Mensagem = "Endereço salvo";
                }
                catch (GraxeiException graxeiException)
                {
                    statusOperacao.Mensagem = graxeiException.Message;
                }
                catch (Exception)
                {
                    statusOperacao.Mensagem = "Ocorreu um erro ao salvar o endereço. Por favor, contate-nos";
                }
            }

            ModelState.Clear();
            return new JsonNetResult(statusOperacao, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
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
        private readonly ITransformacaoMutua<Endereco, EnderecoVistaContrato> _transformacaoEndereco;
    }
}
