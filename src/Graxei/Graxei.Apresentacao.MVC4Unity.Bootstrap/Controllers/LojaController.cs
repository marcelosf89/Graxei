using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using System.Web.Mvc;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Controllers
{
    public class LojaController : Controller
    {
        public LojaController(IServicoLojas servicoLojas)
        {
            _servicoLojas = servicoLojas;
        }

        [HttpGet]
        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Novo(Loja loja)
        {
            _servicoLojas.Salvar(loja);
            return View();
        }

        public ActionResult NovoEndereco()
        {
            return PartialView();
        }


        private IServicoLojas _servicoLojas;
    }
}
