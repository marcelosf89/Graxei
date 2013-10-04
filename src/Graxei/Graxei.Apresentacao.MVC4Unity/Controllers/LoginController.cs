using System.Web.Mvc;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;

namespace Graxei.Apresentacao.MVC4Unity.Controllers
{
    public class LoginController : Controller
    {

        public LoginController(IConsultasLogin consultasUsuarios)
        {
            _consultasLogin = consultasUsuarios;
        }

        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View("Autenticacao");
        }


        [HttpPost]
        public ActionResult Autenticacao(AutenticacaoModel autenticacao)
        {
            /* TODO: ver como será o tratamento de autenticação, que pode (ou poderia) ser login ou e-mail */
            /*try
            {
                if (!ModelState.IsValid)
                {
                    throw new AutenticacaoException("Verifique");
                }
                Usuario usuarioAutenticado = _servicoUsuarios.AutenticarPorLogin(autenticacao.LoginOuEmail, autenticacao.Senha);
                Helper.SetUsuarioLogado(Session, usuarioAutenticado);
            }
            catch (AutenticacaoException ae)
            {
                Response.StatusCode = 500;
                return Content(ae.Message, "text/html");
            }            */

            Usuario usuarioAutenticado = _consultasLogin.AutenticarPorLogin("admingraxei", "graxei");
            Helper.SetUsuarioLogado(Session, usuarioAutenticado);
            return Json(new { url = Url.Action("Home","Administrativo") });
        }

        public ActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public void RedefinirSenha(object obj)
        {
        }

        private IConsultasLogin _consultasLogin;
    }
}
