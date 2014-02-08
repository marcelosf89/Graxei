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
        public ActionResult Index(EnderecosModel item)
        {
            LojaModel model = new LojaModel() { Loja = new Loja(), EnderecosModel = item };
            return View("Novo", model);
        }

        [HttpPost]
        [LimpezaSessaoNovaLoja]
        public ActionResult Novo(UsuarioLogado usuario, EnderecosModel enderecosModel, LojaModel item, HttpPostedFileBase imagem)
        {
            item.EnderecosModel = enderecosModel;
            if (!ModelState.IsValid)
            {
                return PartialView(item);
            }

            try
            {
                // Recuperando os endereços inseridos
                IList<Endereco> enderecos = (from i in enderecosModel.Enderecos
                                             select i.Endereco).ToList();
                item.Loja.AdicionarEnderecos(enderecos);
                _gerenciamentoLojas.SalvarLoja(item.Loja, usuario.Usuario);
            }
            catch (OperacaoEntidadeException ee)
            {
                ModelState.AddModelError("", ee.Message);
                return PartialView(item);
            }
            return PartialView("Incluida", item);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioLogado usuario, EnderecosModel enderecosModel, LojaModel item)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(item);
            }
            try
            {
                // Recuperando os endereços inseridos
                IList<Endereco> enderecos = (from i in enderecosModel.Enderecos
                                             select i.Endereco).ToList();
                item.Loja.AdicionarEnderecos(enderecos);
                _gerenciamentoLojas.SalvarLoja(item.Loja, usuario.Usuario);
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
