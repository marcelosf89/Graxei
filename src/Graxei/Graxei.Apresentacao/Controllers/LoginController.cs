using System.Web.Mvc;
using System.Web.Routing;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.Models;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;
using Graxei.Transversais.Utilidades.Excecoes;
//using Microsoft.Web.WebPages.OAuth;
using DotNetOpenAuth.AspNet;
using System.Web.Security;
using DotNetOpenAuth.GoogleOAuth2;

namespace Graxei.Apresentacao.Controllers
{
    public class LoginController : Controller
    {
        public LoginController(IConsultaLogin consultasUsuarios, IGerenciadorAutenticacao gerenciadorAutenticacao)
        {
            _consultasLogin = consultasUsuarios;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
        }

        //
        // GET: /Login/
        [AllowAnonymous]
        public ActionResult Index()
        {
            //return RedirectToAction("Autenticacao");
            Usuario usuarioAutenticado = _consultasLogin.AutenticarPorLogin("admingraxei", "graxei");
            _gerenciadorAutenticacao.Registrar(usuarioAutenticado);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Autenticacao()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult ContaGoogle()
        {

            return new ExternalLoginResult("Google", Url.Action("ExternalLoginCallback", new { ReturnUrl = "Home" }));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            GoogleOAuth2Client.RewriteRequest();
            //AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            //if (!result.IsSuccessful)
            //{
                return RedirectToAction("ExternalLoginFailure");
            //}

            //Usuario usuarioAutenticado = _consultasLogin.GetPorEmail(result.ExtraData["email"]);
            //if (usuarioAutenticado == null)
                throw new System.Exception("O Usuario não existe");

           // FormsAuthentication.SetAuthCookie(usuarioAutenticado.Nome, false);

            //if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
               // OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // User is new, ask for their desired membership name
                //string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                //ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return RedirectToAction("Index", "Home");
            }
        }
        [AllowAnonymous]
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Autenticacao(AutenticacaoModel autenticacao)
        {
            /* TODO: ver como será o tratamento de autenticação, que pode (ou poderia) ser login ou e-mail */
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(autenticacao);
                }
                Usuario usuarioAutenticado = _consultasLogin.AutenticarPorLogin(autenticacao.LoginOuEmail, autenticacao.Senha);
                FormsAuthentication.SetAuthCookie(usuarioAutenticado.Nome, false);

                _gerenciadorAutenticacao.Registrar(usuarioAutenticado);

            }
            catch (AutenticacaoException ae)
            {
                ViewBag.Mensagem = ae.Message;
                return PartialView(autenticacao);
            }
            return Json(new { url = Url.Action("Index", "Home") });

            /*Usuario usuarioAutenticado = _consultasLogin.AutenticarPorLogin("admingraxei", "graxei");
            Helper.SetUsuarioLogado(Session, usuarioAutenticado);
            return Json(new { url = Url.Action("Home","Administrativo") });*/
        }

        [AllowAnonymous]
        public ActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public void RedefinirSenha(object obj)
        {
        }

        private IConsultaLogin _consultasLogin;
        private IGerenciadorAutenticacao _gerenciadorAutenticacao;

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
               // OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }
    }
}
