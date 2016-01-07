using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Transversais.Comum;
using Graxei.Transversais.Comum.Autenticacao.Interfaces;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Areas.Administrativo.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(IConsultasProdutoVendedor consultasProdutoVendedor, IConsultaLogin consultasUsuarios, IGerenciadorAutenticacao gerenciadorAutenticacao)
        {
            _consultasProdutoVendedor = consultasProdutoVendedor;
            _consultasLogin = consultasUsuarios;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
        }
        //
        // GET: /Administrativo/Home/
        
        public ActionResult Index()
        {
            if (Session[Constantes.UsuarioAtual] == null && User.Identity.IsAuthenticated)
            {
                Usuario usuarioAutenticado = _consultasLogin.GetPorNome(User.Identity.Name);
                _gerenciadorAutenticacao.Registrar(usuarioAutenticado);
            }
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult _Index()
        {
            return PartialView();
        }


        public ActionResult GetQuantidadeProdutos()
        {
           long quantidadeProduto =  _consultasProdutoVendedor.GetQuantidadeProduto();
           return Json(quantidadeProduto, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQuantidadeProdutosCaro()
        {
            long quantidadeProduto = _consultasProdutoVendedor.GetQuantidadeProduto();
            return Json(quantidadeProduto, JsonRequestBehavior.AllowGet);
        }

        private IConsultasProdutoVendedor _consultasProdutoVendedor;
        private IConsultaLogin _consultasLogin;
        private IGerenciadorAutenticacao _gerenciadorAutenticacao;

    }
}
