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
//Mysql            container.RegisterType<IRepositorioUsuarios, UsuariosNHibernateMySQL>()
            container.RegisterType<IRepositorioUsuarios, UsuariosNHibernatePostgre>()
                     .RegisterType<IServicoUsuarios, ServicoUsuarios>(
                            /*new InjectionFactory(p => new ServicoUsuarios(container.Resolve<IRepositorioUsuarios>()))*/
                            );

            // Lojas
//Mysql            container.RegisterType<IRepositorioLojas, LojasNHibernateMySQL>()
            container.RegisterType<IRepositorioLojas, LojasNHibernatePostgre>()
                     .RegisterType<IServicoLojas, ServicoLojas>(
                            /*new InjectionFactory(p => new ServicoLojas(container.Resolve<IRepositorioLojas>(),
                                container.Resolve<IServicoLojaUsuario>(), container.Resolve<IServicoUsuarios>()))*/
                            );

            // Produtos
//Mysql              container.RegisterType<IRepositorioProdutos, ProdutosNHibernateMySQL>()
            container.RegisterType<IRepositorioProdutos, ProdutosNHibernatePostgre>()
                     .RegisterType<IServicoProdutos, ServicoProdutos>(
                            /*new InjectionFactory(p => new ServicoProdutos(container.Resolve<IRepositorioProdutos>()))*/
                            );

            // ProdutosVendedores
  //Mysql             container.RegisterType<IRepositorioProdutoVendedor, ProdutoVendedorNHibernateMySQL>()
            container.RegisterType<IRepositorioProdutoVendedor, ProdutoVendedorNHibernatePostgre>()
                     .RegisterType<IServicoProdutoVendedor, ServicoProdutoVendedor>();

            //Mysql container.RegisterType<IRepositorioAtributos, AtributosNHibernateMySQL>()
            container.RegisterType<IRepositorioAtributos, AtributosNHibernatePostgre>()
                     .RegisterType<IServicoAtributos, ServicoAtributos>();

            // Fabricantes
          //Mysql  container.RegisterType<IRepositorioFabricantes, FabricantesNHibernateMySQL>()
            container.RegisterType<IRepositorioFabricantes, FabricantesNHibernatePostgre>()
                     .RegisterType<IServicoFabricantes, ServicoFabricantes>(
                            /*new InjectionFactory(p => new ServicoFabricantes(container.Resolve<IRepositorioFabricantes>()))*/
                            );

            // Unidades de Medida
            container.RegisterType<IServicoUnidadeMedida, ServicoUnidadeMedida>();

            // Estados
//mysql            container.RegisterType<IRepositorioEstados, EstadosNHibernateMySQL>()
            container.RegisterType<IRepositorioEstados, EstadosNHibernatePostgre>()
                     .RegisterType<IServicoEstados, ServicoEstados>(
                            /*new InjectionFactory(p => new ServicoEstados(container.Resolve<IRepositorioEstados>()))*/
                            );

            // Cidades
//Mysql            container.RegisterType<IRepositorioCidades, CidadesNHibernateMySQL>()
            container.RegisterType<IRepositorioCidades, CidadesNHibernatePostgre>()
                     .RegisterType<IServicoCidades, ServicoCidades>(
                            /*new InjectionFactory(p => new ServicoCidades(container.Resolve<IRepositorioCidades>()))*/
                            );

            // Bairros
//Mysql            container.RegisterType<IRepositorioBairros, BairrosNHibernateMySQL>()
            container.RegisterType<IRepositorioBairros, BairrosNHibernatePostgre>()
                     .RegisterType<IServicoBairros, ServicoBairros>(
                            /*new InjectionFactory(p => new ServicoBairros(container.Resolve<IRepositorioBairros>()))*/
                            );

            // Logradouros
//Mysql            container.RegisterType<IRepositorioLogradouros, LogradourosNHibernateMySQL>()
            container.RegisterType<IRepositorioLogradouros, LogradourosNHibernatePostgre>()
                     .RegisterType<IServicoLogradouros, ServicoLogradouros>(
                            /*new InjectionFactory(p => new ServicoLogradouros(container.Resolve<IRepositorioLogradouros>()))*/);

            // Telefones
//Mysql            container.RegisterType<IRepositorioTelefones, TelefonesNHibernateMySQL>()
            container.RegisterType<IRepositorioTelefones, TelefonesNHibernatePostgre>()
                .RegisterType<IServicoTelefones, ServicoTelefones>(
                            /*new InjectionFactory(p => new ServicoTelefones(container.Resolve<IRepositorioTelefones>()))*/);

            // Endereços
//Mysql            container.RegisterType<IRepositorioEnderecos, EnderecosNHibernateMySQL>()
            container.RegisterType<IRepositorioEnderecos, EnderecosNHibernatePostgre>()
                     .RegisterType<IServicoEnderecos, ServicoEnderecos>(
                            /*new InjectionFactory(p => 
                                new ServicoEnderecos(
                                       container.Resolve<IRepositorioEnderecos>(),
                                       container.Resolve<IServicoLogradouros>(), 
                                       container.Resolve<IServicoBairros>(), 
                                       container.Resolve<IServicoCidades>(),
                                       container.Resolve<IServicoEstados>()))*/
                            );

//Mysql            container.RegisterType<IRepositorioTiposTelefone, TiposTelefoneNHibernateMySQL>()
            container.RegisterType<IRepositorioTiposTelefone, TiposTelefoneNHibernatePostgre>()
                     .RegisterType<IServicoTiposTelefone, ServicoTiposTelefone>();

//            container.RegisterType<IRepositorioListaLojas, ListaLojasNHibernateMySQL>()
            container.RegisterType<IRepositorioListaLojas, ListaLojasNHibernatePostgre>()
                .RegisterType<IServicoListaLojas, ServicoListaLojas>();
        }

    }
}