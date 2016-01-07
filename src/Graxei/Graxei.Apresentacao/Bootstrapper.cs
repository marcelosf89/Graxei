using System.Web.Mvc;
using Graxei.Aplicacao.Fabrica;
using Graxei.Apresentacao.Infrastructure;
using Graxei.Transversais.Comum.Autenticacao;
using Graxei.Transversais.Comum.Autenticacao.Interfaces;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Microsoft.Practices.Unity.InterceptionExtension;
using Graxei.Transversais.Comum.Data;
using Graxei.Apresentacao.Areas.Administrativo.Infraestutura.Cache;
using Graxei.Transversais.Comum.TransformacaoDados.Interface;
using Graxei.Modelo;
using Graxei.Apresentacao.Infrastructure.Cache;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Comum.LogAplicacao;
using Graxei.Transversais.Comum.LogAplicacao.Log4Net;

namespace Graxei.Apresentacao
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
               .RegisterInstance<ILogAplicacao>(new Log4NetImpl())
               .RegisterType<ICacheComum, CacheComumHttpSession>(new InjectionConstructor())
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