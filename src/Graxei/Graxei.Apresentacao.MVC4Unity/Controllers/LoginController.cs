﻿using System.Web.Mvc;
using System.Web.Routing;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;
using Graxei.Transversais.Utilidades.Excecoes;
using Microsoft.Web.WebPages.OAuth;

namespace Graxei.Apresentacao.MVC4Unity.Controllers
{
    public class LoginController : Controller
    {

        public LoginController(IConsultasLogin consultasUsuarios, IGerenciadorAutenticacao gerenciadorAutenticacao)
        {
            _consultasLogin = consultasUsuarios;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
        }

        //
        // GET: /Login/

        public ActionResult Index()
        {
            //return RedirectToAction("Autenticacao");
            Usuario usuarioAutenticado = _consultasLogin.AutenticarPorLogin("admingraxei", "graxei");
            _gerenciadorAutenticacao.Registrar(usuarioAutenticado);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Autenticacao()
        {
            return View();
        }
        public ActionResult ContaGoogle()
        {
                    
            return new ExternalLoginResult("Google", Url.Action("ExternalLoginCallback", new { ReturnUrl = "Home" }));
            //OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return Json(new { url = Url.Action("Home", "Administrativo") });
                //return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                //return RedirectToLocal(returnUrl);
                return Json(new { url = Url.Action("Home", "Administrativo") });
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                //return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
                return Json(new { url = Url.Action("Home", "Administrativo") });
            }
        }


        [HttpPost]
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
                _gerenciadorAutenticacao.Registrar(usuarioAutenticado);
            }
            catch (AutenticacaoException ae)
            {
                ViewBag.Mensagem = ae.Message;
                return PartialView(autenticacao);
            }
            return Json(new {url = Url.Action("Home", "Administrativo")});

            /*Usuario usuarioAutenticado = _consultasLogin.AutenticarPorLogin("admingraxei", "graxei");
            Helper.SetUsuarioLogado(Session, usuarioAutenticado);
            return Json(new { url = Url.Action("Home","Administrativo") });*/
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
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }
    }
}
