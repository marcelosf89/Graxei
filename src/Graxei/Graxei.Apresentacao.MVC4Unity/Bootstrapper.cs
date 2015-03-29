using System.Web.Mvc;
using Graxei.Aplicacao.Fabrica;
using Graxei.Apresentacao.MVC4Unity.Infrastructure;
using Graxei.Transversais.Utilidades.Autenticacao;
using Graxei.Transversais.Utilidades.Autenticacao.Interfaces;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Microsoft.Practices.Unity.InterceptionExtension;
using Graxei.Transversais.Utilidades.Data;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.Cache;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;
using Graxei.Modelo;
using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;

namespace Graxei.Apresentacao.MVC4Unity
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      IUnityContainer container = new UnityContainer();
      container.AddNewExtension<Interception>();
      container.RegisterType<EnderecosViewModelEntidade>()
               .RegisterType<IGerenciadorAutenticacao, GerenciadorAutenticacaoSessaoHttp>()
               .RegisterType<IDataSistema, DataSistemaPadrao>()
               .RegisterType<ICacheElementosEndereco, CacheEnderecosSessaoHttp>(new InjectionConstructor())
               .RegisterType<ITransformacaoMutua<Endereco, EnderecoVistaContrato>, EnderecosViewModelEntidade>();
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        BootstrapperAplicacao.RegisterTypes(container);
    }

  }

}