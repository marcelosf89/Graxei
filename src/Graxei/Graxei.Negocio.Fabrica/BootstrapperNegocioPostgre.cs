using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlResolver.Factory;
using Graxei.Persistencia.Implementacao.NHibernate;
using Graxei.Persistencia.Implementacao.NHibernate.Postgre;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Fabrica
{
    public class BootstrapperNegocioPostgre
    {
        public static void Register(IUnityContainer container)
        {
            // Usuários
            container.RegisterType<IRepositorioUsuarios, UsuariosNHibernatePostgre>()
                 .RegisterType<IServicoUsuarios, ServicoUsuarios>();

            // Lojas
            container.RegisterType<IRepositorioLojas, LojasRepositorio>()
                       .RegisterType<IServicoLojas, ServicoLojas>();

            // Produtos
            container.RegisterType<IRepositorioProdutos, ProdutosRepositorio>()
                     .RegisterType<IServicoProdutos, ServicoProdutos>();

            // ProdutosVendedores
            container.RegisterType<IRepositorioProdutoVendedor, ProdutoVendedorRepositorio>()
                  .RegisterType<IServicoProdutoVendedor, ServicoProdutoVendedor>();

            // Atributos
            container.RegisterType<IRepositorioAtributos, AtributosNHibernatePostgre>()
               .RegisterType<IServicoAtributos, ServicoAtributos>();

            // Fabricantes
            container.RegisterType<IRepositorioFabricantes, FabricantesNHibernatePostgre>()
                       .RegisterType<IServicoFabricantes, ServicoFabricantes>();

            // Estados
            container.RegisterType<IRepositorioEstados, EstadosNHibernatePostgre>()
                                 .RegisterType<IServicoEstados, ServicoEstados>();

            // Cidades
            container.RegisterType<IRepositorioCidades, CidadesNHibernatePostgre>()
                       .RegisterType<IServicoCidades, ServicoCidades>();

            // Bairros
            container.RegisterType<IRepositorioBairros, BairrosNHibernatePostgre>()
                       .RegisterType<IServicoBairros, ServicoBairros>();

            // Logradouros
            container.RegisterType<IRepositorioLogradouros, LogradourosNHibernatePostgre>()
                       .RegisterType<IServicoLogradouros, ServicoLogradouros>();

            // Telefones
            container.RegisterType<IRepositorioTelefones, TelefonesNHibernatePostgre>()
                  .RegisterType<IServicoTelefones, ServicoTelefones>();

            // Endereços
            container.RegisterType<IRepositorioEnderecos, Enderecos>()
                       .RegisterType<IServicoEnderecos, ServicoEnderecos>();

            // Telefone
            container.RegisterType<IRepositorioTiposTelefone, TiposTelefoneNHibernatePostgre>()
                       .RegisterType<IServicoTiposTelefone, ServicoTiposTelefone>();

            //Lista lojas
            container.RegisterType<IRepositorioListaLojas, ListaLojasRepositorio>()
                       .RegisterType<IServicoListaLojas, ServicoListaLojas>();

            container.RegisterType<IRepositorioListaProdutosLoja, ListaProdutosLojaRepositorio>()
                       .RegisterType<IServicoListaProdutosLoja, ServicoListaProdutosLojaUmEndereco>();

            container.RegisterType<IRepositorioPlanos, PlanosNHibernatePostgre>()
                       .RegisterType<IServicoPlanos, ServicoPlanos>();

            container.RegisterType<IListaProdutosLojaSqlResolverFactory, ListaProdutosLojaSqlResolverFactory>();

            container.RegisterType<IVisitorCriacaoFuncao, VisitorFuncoesComVetorDeTipos>();
        }
    }
}
