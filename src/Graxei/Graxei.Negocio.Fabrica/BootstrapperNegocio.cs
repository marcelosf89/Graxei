using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao;
using Graxei.Persistencia.Implementacao.NHibernate;
using Microsoft.Practices.Unity;

namespace Graxei.Negocio.Fabrica
{
    public static class BootstrapperNegocio
    {

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IServicosFabrica, ServicoFabricaUnity>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRepositorioProdutos, ProdutosNHibernateMySQL>()
                     .RegisterType<IServicoProdutos, ServicoProdutos>(
                            new InjectionFactory(p => new ServicoProdutos(container.Resolve<IRepositorioProdutos>())));
            container.RegisterType<IRepositorioFabricantes, FabricantesNHibernateMySQL>()
                     .RegisterType<IServicoFabricantes, ServicoFabricantes>(
                            new InjectionFactory(p => new ServicoFabricantes(container.Resolve<IRepositorioFabricantes>())));
        }

    }
}