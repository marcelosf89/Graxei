using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.NHibernate;
using Microsoft.Practices.Unity;

namespace Graxei.Negocio.Fabrica
{
    public static class BootstrapperNegocio
    {

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IServicosFabrica, ServicoFabricaUnity>(new ContainerControlledLifetimeManager());
            
            // Usuários
            container.RegisterType<IRepositorioUsuarios, UsuariosNHibernateMySQL>()
                     .RegisterType<IServicoUsuarios, ServicoUsuarios>(
                            /*new InjectionFactory(p => new ServicoUsuarios(container.Resolve<IRepositorioUsuarios>()))*/
                            );

            //LojaUsuario
            container.RegisterType<IRepositorioLojaUsuario, LojaUsuarioNHibernateMySQL>()
                     .RegisterType<IServicoLojaUsuario, ServicoLojaUsuario>(
                         /*new InjectionFactory(p => new ServicoLojaUsuario(container.Resolve<IRepositorioLojaUsuario>()))*/
                         );
            // Lojas
            container.RegisterType<IRepositorioLojas, LojasNHibernateMySQL>()
                     .RegisterType<IServicoLojas, ServicoLojas>(
                            /*new InjectionFactory(p => new ServicoLojas(container.Resolve<IRepositorioLojas>(),
                                container.Resolve<IServicoLojaUsuario>(), container.Resolve<IServicoUsuarios>()))*/
                            );

            // Produtos
            container.RegisterType<IRepositorioProdutos, ProdutosNHibernateMySQL>()
                     .RegisterType<IServicoProdutos, ServicoProdutos>(
                            /*new InjectionFactory(p => new ServicoProdutos(container.Resolve<IRepositorioProdutos>()))*/
                            );

            // ProdutosVendedores
            container.RegisterType<IRepositorioProdutoVendedor, ProdutoVendedorNHibernateMySQL>()
                     .RegisterType<IServicoProdutoVendedor, ServicoProdutoVendedor>();

            // Fabricantes
            container.RegisterType<IRepositorioFabricantes, FabricantesNHibernateMySQL>()
                     .RegisterType<IServicoFabricantes, ServicoFabricantes>(
                            /*new InjectionFactory(p => new ServicoFabricantes(container.Resolve<IRepositorioFabricantes>()))*/
                            );

            // Estados
            container.RegisterType<IRepositorioEstados, EstadosNHibernateMySQL>()
                     .RegisterType<IServicoEstados, ServicoEstados>(
                            /*new InjectionFactory(p => new ServicoEstados(container.Resolve<IRepositorioEstados>()))*/
                            );

            // Cidades
            container.RegisterType<IRepositorioCidades, CidadesNHibernateMySQL>()
                     .RegisterType<IServicoCidades, ServicoCidades>(
                            /*new InjectionFactory(p => new ServicoCidades(container.Resolve<IRepositorioCidades>()))*/
                            );

            // Bairros
            container.RegisterType<IRepositorioBairros, BairrosNHibernateMySQL>()
                     .RegisterType<IServicoBairros, ServicoBairros>(
                            /*new InjectionFactory(p => new ServicoBairros(container.Resolve<IRepositorioBairros>()))*/
                            );

            // Logradouros
            container.RegisterType<IRepositorioLogradouros, LogradourosNHibernateMySQL>()
                     .RegisterType<IServicoLogradouros, ServicoLogradouros>(
                            /*new InjectionFactory(p => new ServicoLogradouros(container.Resolve<IRepositorioLogradouros>()))*/);

            // Telefones
            container.RegisterType<IRepositorioTelefones, TelefonesNHibernateMySQL>()
                .RegisterType<IServicoTelefones, ServicoTelefones>(
                            /*new InjectionFactory(p => new ServicoTelefones(container.Resolve<IRepositorioTelefones>()))*/);

            // Endereços
            container.RegisterType<IRepositorioEnderecos, EnderecosNHibernateMySQL>()
                     .RegisterType<IServicoEnderecos, ServicoEnderecos>(
                            /*new InjectionFactory(p => 
                                new ServicoEnderecos(
                                       container.Resolve<IRepositorioEnderecos>(),
                                       container.Resolve<IServicoLogradouros>(), 
                                       container.Resolve<IServicoBairros>(), 
                                       container.Resolve<IServicoCidades>(),
                                       container.Resolve<IServicoEstados>()))*/
                            );
        }

    }
}