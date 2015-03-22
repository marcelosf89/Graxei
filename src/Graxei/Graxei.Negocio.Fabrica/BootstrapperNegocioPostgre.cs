using Graxei.Negocio.Contrato;
using Graxei.Negocio.Contrato.Factories;
using Graxei.Negocio.Implementacao;
using Graxei.Negocio.Implementacao.Factories;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.AlteracaoProduto.Visitor;
using Graxei.Persistencia.Implementacao.FluentNHibernate.Postgre.SqlNativo;
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
            container.RegisterType<IRepositorioUsuarios, UsuariosNHibernatePostgre>()
                      .RegisterType<IServicoUsuarios, ServicoUsuarios>()
                      .RegisterType<IRepositorioLojas, LojasRepositorio>()
                      .RegisterType<IServicoLojas, ServicoLojas>()
                      .RegisterType<IRepositorioProdutos, ProdutosRepositorio>()
                      .RegisterType<IServicoProdutos, ServicoProdutos>()
                      .RegisterType<IRepositorioProdutoVendedor, ProdutoVendedorRepositorio>()
                      .RegisterType<IProdutoVendedorNativo, ProdutoVendedorNativo>()
                      .RegisterType<IServicoProdutoVendedor, ServicoProdutoVendedor>()
                      .RegisterType<IRepositorioAtributos, AtributosNHibernatePostgre>()
                      .RegisterType<IServicoAtributos, ServicoAtributos>()
                      .RegisterType<IRepositorioFabricantes, FabricantesNHibernatePostgre>()
                      .RegisterType<IServicoFabricantes, ServicoFabricantes>()
                      .RegisterType<IRepositorioEstados, EstadosNHibernatePostgre>()
                      .RegisterType<IServicoEstados, ServicoEstados>()
                      .RegisterType<IRepositorioCidades, CidadesNHibernatePostgre>()
                      .RegisterType<IServicoCidades, ServicoCidades>()
                      .RegisterType<IRepositorioBairros, BairrosNHibernatePostgre>()
                      .RegisterType<IServicoBairros, ServicoBairros>()
                      .RegisterType<IRepositorioLogradouros, LogradourosNHibernatePostgre>()
                      .RegisterType<IServicoLogradouros, ServicoLogradouros>()
                      .RegisterType<IRepositorioTelefones, TelefonesNHibernatePostgre>()
                      .RegisterType<IServicoTelefones, ServicoTelefones>()
                      .RegisterType<IRepositorioEnderecos, EnderecosRepositorio>()
                      .RegisterType<IServicoEnderecos, ServicoEnderecos>()
                      .RegisterType<IRepositorioTiposTelefone, TiposTelefoneNHibernatePostgre>()
                      .RegisterType<IServicoTiposTelefone, ServicoTiposTelefone>()
                      .RegisterType<IRepositorioListaLojas, ListaLojasRepositorio>()
                      .RegisterType<IServicoListaLojas, ServicoListaLojas>()
                      .RegisterType<IRepositorioListaProdutosLoja, ListaProdutosLojaRepositorio>()
                      .RegisterType<IServicoListaProdutosLoja, ServicoListaProdutosLojaUmEndereco>()
                      .RegisterType<IRepositorioPlanos, PlanosNHibernatePostgre>()
                      .RegisterType<IServicoPlanos, ServicoPlanos>()
                      .RegisterType<IListaProdutosLojaSqlResolverFactory, ListaProdutosLojaSqlResolverFactory>()
                      .RegisterType<IVisitorCriacaoFuncao, VisitorFuncoesComVetorDeTipos>()
                      .RegisterType<IMudancaProdutoVendedorFuncaoFactory, MudancaoProdutoVendedorFuncaoFactory>()
                      .RegisterType<IBairrosBuilder, BairrosBuilder>()
                      .RegisterType<IEnderecosBuilder, EnderecosBuilder>();
        }
    }
}
