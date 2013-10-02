using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graxei.Apresentacao.MVC4Unity.Models;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Apresentacao.MVC4Unity.Controllers
{
    public class LoginController : Controller
    {

        public LoginController(IServicoUsuarios servicoUsuarios)
        {
            _servicoUsuarios = servicoUsuarios;
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

            Usuario usuarioAutenticado = _servicoUsuarios.AutenticarPorLogin("admingraxei", "graxei");
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

        private IServicoUsuarios _servicoUsuarios;
    }
}
