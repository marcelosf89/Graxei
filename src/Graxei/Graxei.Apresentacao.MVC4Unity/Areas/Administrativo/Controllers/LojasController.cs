using System.Collections.Generic;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Apresentacao.MVC4Unity.Infrastructure;
using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.TinyTypes;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class LojasController : Controller
    {
        public LojasController(IConsultasEnderecos consultasEnderecos, IGerenciamentoLojas gerenciamentoLojas,
            IConsultasLojas consultasLojas, ITransformacaoMutua<Loja, LojaContrato> transformacaoMutuaLojas,
            IConsultasEstados consultasEstados, IConsultasListaLojas consultasListaLojas)
        {
            _gerenciamentoLojas = gerenciamentoLojas;
            _consultasLojas = consultasLojas;
            _transformacaoMutuaLojas = transformacaoMutuaLojas;
            _consultasEstados = consultasEstados;
            _consultasEnderecos = consultasEnderecos;
            _consultasListaLojas = consultasListaLojas;
        }

        #region ActionResults

        public ActionResult Index(EnderecoModel item)
        {
            var model = new LojaModel {LojaContrato = new LojaContrato(), EnderecoModel = item};
            return View("Loja", model);
        }

        [HttpGet]
        public ActionResult Editar(long idLoja)
        {
            Loja loja = _consultasLojas.Get(idLoja);
            if (loja != null)
            {
                LojaContrato lojaContrato = _transformacaoMutuaLojas.Transformar(loja);

                var model = new LojaModel {LojaContrato = lojaContrato, EnderecoModel = null};
                return View("Loja", model);
            }
            return null;
        }

        public ActionResult Listar(int numeroPagina, int tamanho)
        {
            if (numeroPagina < 0)
            {
                numeroPagina = 0;
            }
            ListaLojas listaLojas = _consultasListaLojas.Get(numeroPagina, tamanho);
            List<ListaLojasContrato> listaLojasContrato = new List<ListaLojasContrato>();
            listaLojasContrato.AddRange(listaLojas.Lista);
            //listaLojas = new ListaLojas(listaLojasContrato, new ListaTotalElementos(130), new ListaElementoAtual(numeroPagina));

            ViewBag.Page = numeroPagina;
            return View("Listar", listaLojas);
        }

        [HttpPost]
        [LimpezaSessaoNovaLoja]
        public ActionResult Salvar(Usuario usuario, LojaModel item)
        {
            if (item != null && item.LojaContrato != null && item.LojaContrato.Id > 0)
            {
                return EditarNova(usuario, item);
            }
            if (!ModelState.IsValid)
            {
                return PartialView("NovaLojaAjax", item);
            }
            LojaContrato lojaSalva;
            try
            {
                lojaSalva = _gerenciamentoLojas.Salvar(item.LojaContrato, usuario);
            }
            catch (OperacaoEntidadeException ee)
            {
                ModelState.AddModelError(string.Empty, ee.Message);
                return PartialView("NovaLojaAjax", item);
            }
            ModelState.Clear();
            item.LojaContrato = lojaSalva;
            ViewBag.OperacaoSucesso = Sucesso.LojaIncluida;
            return PartialView("NovaLojaAjax", item);
        }

        [HttpPost]
        [LimpezaSessaoNovaLoja]
        public ActionResult EditarNova(Usuario usuario, LojaModel item)
        {
            if (item == null) { item = new LojaModel(); }
            if (!ModelState.IsValid)
            {
                return PartialView("NovaLojaAjax", item);
            }
            LojaContrato lojaSalva;
            try
            {
                lojaSalva = _gerenciamentoLojas.Salvar(item.LojaContrato, usuario);
                lojaSalva = _consultasLojas.GetComEnderecos(lojaSalva.Id);
            }
            catch (OperacaoEntidadeException ee)
            {
                ModelState.AddModelError(string.Empty, ee.Message);
                return PartialView("NovaLojaAjax", item);
            }
            ModelState.Clear();
            item.LojaContrato = lojaSalva;

            ViewBag.OperacaoSucesso = Sucesso.LojaAtualizada;
            return PartialView("NovaLojaAjax", item);
        }

        public FileContentResult GetImagem(int idLoja = 0)
        {
            if (idLoja != 0)
            {
                Loja loja = _consultasLojas.Get(idLoja);
                if (loja != null)
                {
                    return File(loja.Logotipo, "image/jpeg");
                }
            }

            return null;
        }

        #endregion

        #region Atributos Privados

        private readonly IConsultasLojas _consultasLojas;
        private readonly IGerenciamentoLojas _gerenciamentoLojas;
        private readonly ITransformacaoMutua<Loja, LojaContrato> _transformacaoMutuaLojas;
        private IConsultasEnderecos _consultasEnderecos;
        private IConsultasEstados _consultasEstados;
        private IConsultasListaLojas _consultasListaLojas;
        private ITransformacaoMutua<Endereco, EnderecosViewModelEntidade> _transformacaoMutuaEnderecos;

        #endregion
    }
}