﻿using System;
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

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class EnderecosController : Controller
    {

        public EnderecosController(IConsultasEnderecos consultasEnderecos, IConsultasBairros consultasBairros, IConsultasLojas consultasLojas, IConsultasEstados consultasEstados, IConsultasCidades consultasCidades, IConsultasLogradouros consultasLogradouros, IGerenciamentoEnderecos gerenciamentoEnderecos, IConsultasTiposTelefone consultasTiposTelefone)
        {
            _consultaEnderecos = consultasEnderecos;
            _consultasBairros = consultasBairros;
            _consultasLojas = consultasLojas;
            _consultasEstados = consultasEstados;
            _consultasCidades = consultasCidades;
            _consultasLogradouros = consultasLogradouros;
            _gerenciamentoEnderecos = gerenciamentoEnderecos;
            _consultasTiposTelefone = consultasTiposTelefone;
        }

        public ActionResult EditarEndereco(long idEndereco)
        {
            ModelState.Clear();
            EnderecoModel item;
            Endereco endereco = _consultaEnderecos.Get(idEndereco);
            EnderecosViewModelEntidade transformacao = new EnderecosViewModelEntidade(_consultasBairros, _consultaEnderecos, _consultasTiposTelefone);
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
            EnderecoModel item = new EnderecoModel();
            item.IdLoja = idLoja;

            IList<Estado> estados = _consultasEstados.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return PartialView("ModalEndereco", item);
        }

        [HttpPost]
        public ActionResult NovoEnderecoRetorno(EnderecoModel endereco)
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
            EnderecosViewModelEntidade transformacao = new EnderecosViewModelEntidade(_consultasBairros, _consultaEnderecos, _consultasTiposTelefone);
            EnderecoModel item = transformacao.Transformar(endereco);
            return PartialFormularioEndereco(item);
        }

        private ActionResult PartialFormularioEndereco(EnderecoModel item)
        {
            IList<Estado> estados = _consultasEstados.GetEstados(EstadoOrdem.Sigla);
            ViewBag.Estados = new SelectList(estados, "Id", "Sigla");
            return PartialView("FormularioEndereco", item);
        }

        [HttpPost]
        public ActionResult Salvar(EnderecoModel enderecoModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialFormularioEndereco(enderecoModel);
            }
            try
            {
                Bairro bairro = new BairrosBuilder(_consultasBairros, _consultasCidades, _consultasEstados)
                    .SetBairro(enderecoModel.Bairro)
                    .SetCidade(enderecoModel.Cidade)
                    .SetEstado(enderecoModel.IdEstado)
                    .Build();
                Loja loja = _consultasLojas.Get(enderecoModel.IdLoja);
                if (loja == null)
                {
                    throw new OperacaoEntidadeException(string.Format("Loja com id {0} não pôde ser encontrada",
                        enderecoModel.IdLoja));
                }
                EnderecosBuilder enderecosBuilder = new EnderecosBuilder(_consultaEnderecos, _consultasTiposTelefone);
                Endereco endereco = enderecosBuilder
                    .SetId(enderecoModel.Id)
                    .SetLogradouro(enderecoModel.Logradouro)
                    .SetNumero(enderecoModel.Numero)
                    .SetComplemento(enderecoModel.Complemento)
                    .SetLoja(loja)
                    .SetBairro(bairro)
                    .SetCnpj(enderecoModel.Cnpj)
                    .SetTelefones(enderecoModel.Telefones)
                    .Build();
                _gerenciamentoEnderecos.Salvar(endereco, null);
                List<Endereco> enderecos = _consultaEnderecos.GetPorLoja(loja.Id);
                List<EnderecoListaContrato> listaEnderecos = new List<EnderecoListaContrato>();
                foreach (Endereco end in enderecos)
                {
                    listaEnderecos.Add(new EnderecoListaContrato(end.Id, end.ToString(), end.Cnpj));
                }
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
        private readonly IConsultasLogradouros _consultasLogradouros;
        private readonly IConsultasBairros _consultasBairros;
        private readonly IConsultasEstados _consultasEstados;
        private readonly IConsultasLojas _consultasLojas;
        private readonly IConsultasCidades _consultasCidades;
        private readonly IConsultasTiposTelefone _consultasTiposTelefone;
        private readonly IGerenciamentoEnderecos _gerenciamentoEnderecos;

        #endregion

    }
}
