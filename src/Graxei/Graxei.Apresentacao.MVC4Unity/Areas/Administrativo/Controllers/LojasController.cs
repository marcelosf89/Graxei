using System.Collections.Generic;
using System.Linq;
using System.Web;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using System.Web.Mvc;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class LojasController : Controller
    {
        public LojasController(IGerenciamentoLojas gerenciamentoLojas, IConsultasLojas consultasLojas)
        {
            _gerenciamentoLojas = gerenciamentoLojas;
            _consultasLojas = consultasLojas;
        }

        #region ActionResults
        public ActionResult Index(EnderecoContrato item)
        {
            LojaModel model = new LojaModel() { LojaContrato = new LojaContrato(), EnderecoContratoForm = item };
            return View("Novo", model);
        }

        [HttpPost]
        [LimpezaSessaoNovaLoja]
        public ActionResult Novo(UsuarioLogado usuario, LojaModel item)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(item);
            }

            try
            {
                _gerenciamentoLojas.SalvarLoja(item.LojaContrato, usuario.Usuario);
            }
            catch (OperacaoEntidadeException ee)
            {
                ModelState.AddModelError("", ee.Message);
                return PartialView(item);
            }
            return PartialView("Incluida", item);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioLogado usuario, LojaModel item)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(item);
            }
            try
            {
                _gerenciamentoLojas.SalvarLoja(item.LojaContrato, usuario.Usuario);
            }
            catch (OperacaoEntidadeException ee)
            {
                ModelState.AddModelError("", ee.Message);
                return PartialView(item);
            }
            ViewBag.Sucesso = Textos.LojaIncluida;
            return PartialView();
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

        #endregion

    }
}
