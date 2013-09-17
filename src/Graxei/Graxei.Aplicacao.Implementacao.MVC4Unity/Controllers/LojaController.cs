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

        public ActionResult Index()
        {
            return View();
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

        private IServicoLojas _servicoLojas;
    }
}
