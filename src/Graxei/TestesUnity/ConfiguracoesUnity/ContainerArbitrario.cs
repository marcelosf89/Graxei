using Microsoft.Practices.Unity;
using TestesUnity.Testes;

namespace TestesUnity.ConfiguracoesUnity
{
    public class ContainerArbitrario
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IAgregador, AgregadorDez>()
                .RegisterType<IDependente, Dependente>();
            //new InjectionFactory(p => new Dependente(container.Resolve<IAgregador>())));
        } 
    }
}
