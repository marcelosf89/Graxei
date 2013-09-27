using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Graxei.Aplicacao.Implementacao.MVC4Unity.Models;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Aplicacao.Implementacao.MVC4Unity.Controllers
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
            try
            {
                Usuario usuarioAutenticado = _servicoUsuarios.AutenticarPorLogin(autenticacao.LoginOuEmail, autenticacao.Senha);
                Session[Constantes.UsuarioAtual] = usuarioAutenticado;
            }
            catch (AutenticacaoException ae)
            {
                return Content(ae.Message);
            }
            return Redirect("~/Administrativo/Home");
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
