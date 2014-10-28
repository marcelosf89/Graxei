using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Apresentacao.MVC4Unity.Infrastructure;
using Graxei.Modelo;
using System.Web.Mvc;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class LojasController : Controller
    {
        public LojasController(IGerenciamentoLojas gerenciamentoLojas, IConsultasLojas consultasLojas, ITransformacaoMutua<Loja, LojaContrato> transformacaoMutuaLojas)
        {
            _gerenciamentoLojas = gerenciamentoLojas;
            _consultasLojas = consultasLojas;
            _transformacaoMutuaLojas = transformacaoMutuaLojas;
        }

        #region ActionResults
        public ActionResult Index(EnderecoModel item)
        {
            LojaModel model = new LojaModel { LojaContrato = new LojaContrato(), EnderecoModel = item };
            return View("Loja", model);
        }

        public ActionResult Editar(long idLoja)
        {
            Loja loja = _consultasLojas.Get(idLoja);
            if (loja != null)
            {
                LojaContrato lojaContrato =  _transformacaoMutuaLojas.Transformar(loja);

                LojaModel model = new LojaModel { LojaContrato = lojaContrato, EnderecoModel = null };
                return View("Loja", model);
            }
            return null;
        }

        [HttpPost]
        [LimpezaSessaoNovaLoja]
        public ActionResult Nova(Usuario usuario, LojaModel item)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("LojaAjax", item);
            }
            LojaContrato lojaSalva = null;
            try
            {
                lojaSalva = _gerenciamentoLojas.Salvar(item.LojaContrato, usuario);
            }
            catch (OperacaoEntidadeException ee)
            {
                ModelState.AddModelError(string.Empty, ee.Message);
                return PartialView("LojaAjax", item);
            }
            ModelState.Clear();
            item.LojaContrato = lojaSalva;

            ViewBag.OperacaoSucesso = Sucesso.LojaIncluida;
            item.EnderecoModel.IdLoja = item.LojaContrato.Id;
            return PartialView("LojaAjax", item);
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
        
        private readonly IGerenciamentoLojas _gerenciamentoLojas;
        private IConsultasLojas _consultasLojas;
        private ITransformacaoMutua<Loja, LojaContrato> _transformacaoMutuaLojas;
        private ITransformacaoMutua<Endereco, EnderecosViewModelEntidade> _transformacaoMutuaEnderecos;
        
        #endregion
    }
}
