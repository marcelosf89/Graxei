﻿using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Binders;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Apresentacao.MVC4Unity.Binders;
using Graxei.Apresentacao.MVC4Unity.Models;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Graxei.Apresentacao.MVC4Unity
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(NovosEnderecosModel), new NovosEnderecosModelBinder());
            ModelBinders.Binders.Add(typeof (UsuarioLogado), new UsuariosLogadoBinder());                    
            /* TODO: retirar esse trecho de código */
            IUnityContainer cont = Bootstrapper.Initialise();
            /*IServicoUsuarios usu = cont.Resolve<IServicoUsuarios>();
            Usuario usuario = usu.GetPorLogin("graxeiadmin");
            Session[Constantes.UsuarioAtual] = usuario;*/
        }
    }
}