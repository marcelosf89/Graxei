using System.Web.Mvc;
using Graxei.Aplicacao.Fabrica;
using Graxei.Apresentacao.MVC4Unity.Infrastructure;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

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
      container.RegisterType<EnderecosViewModelEntidade>();      
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        BootstrapperAplicacao.RegisterTypes(container);
    }

  }

}