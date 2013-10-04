using System.Collections.Generic;
using System.Linq;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using System.Web.Mvc;
using Graxei.Transversais.Utilidades.Excecoes;


namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Controllers
{
    public class LojasController : Controller
    {
        public LojasController(IGerenciamentoLojas gerenciamentoLojas)
        {
            _gerenciamentoLojas = gerenciamentoLojas;
        }

        #region ActionResults
        public ActionResult Index(NovosEnderecosModel item)
        {
            NovaLoja model = new NovaLoja() { Loja = new Loja(), NovosEnderecosModel = item };
            return View("Novo", model);
        }

        [HttpPost]
        public ActionResult Novo(UsuarioLogado usuario, NovaLoja item)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                // Recuperando os endereços inseridos
                IList<Endereco> enderecos = (from i in item.NovosEnderecosModel.Enderecos
                                           select i.Endereco).ToList();

                item.Loja.AdicionarEnderecos(enderecos);
                _gerenciamentoLojas.SalvarLoja(item.Loja, usuario.Usuario);
            }
            catch (OperacaoEntidadeException ee)
            {
                return View("Novo", item);
            }
            return View("Salvo");
        }

        public RedirectToRouteResult NovoEndereco(NovosEnderecosModel enderecos, NovaLoja model)
        {
            return RedirectToAction("Index", "Enderecos");
        }

        #endregion
        #region Atributos Privados
        private readonly IGerenciamentoLojas _gerenciamentoLojas;
        #endregion

    }
}
