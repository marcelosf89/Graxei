using System.Configuration;
using System.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Graxei.FluentNHibernate.Convencoes;
using Graxei.FluentNHibernate.Mapeamento;
using Graxei.FluentNHibernate.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.NHibernate;
using System.Collections.Generic;

namespace Graxei.Negocio.Implementacao.Teste.Integracao
{
    [TestClass]
    public class ImplementacaoLojas
    {
        [TestMethod]
        public void IntegracaoSalvar_LojaSalva()
        {
            Loja loja = new Loja();
            loja.Nome = "Loja do Graxei 2.0";
            IRepositorioProdutoVendedor repositorioProdutoVendedor = new ProdutoVendedorNHibernateMySQL();
            IRepositorioUsuarios repositorioUsuario = new UsuariosNHibernateMySQL();
            IRepositorioLojaUsuario repositorioLojaUsuario = new LojaUsuarioNHibernateMySQL();
            IRepositorioLojas repositorioLojas = new LojasNHibernateMySQL(repositorioProdutoVendedor);
            IRepositorioLogradouros repositorioLogradouros = new LogradourosNHibernateMySQL();
            IRepositorioBairros repositorioBairros = new BairrosNHibernateMySQL();
            IRepositorioCidades repositorioCidades = new CidadesNHibernateMySQL();
            IRepositorioEstados repositorioEstados = new EstadosNHibernateMySQL();
            IRepositorioEnderecos repositorioEnderecos = new EnderecosNHibernateMySQL(repositorioEstados, repositorioCidades, repositorioBairros);
            IServicoLojaUsuario servicoLojaUsuario = new ServicoLojaUsuario(repositorioLojaUsuario);
            IServicoUsuarios servicoUsuarios = new ServicoUsuarios(repositorioUsuario);
            IServicoLogradouros servicoLogradouros = new ServicoLogradouros(repositorioLogradouros);
            IServicoBairros servicoBairros = new ServicoBairros(repositorioBairros);
            IServicoCidades servicoCidades = new ServicoCidades(repositorioCidades);
            IServicoEstados servicoEstados = new ServicoEstados(repositorioEstados);
            IServicoEnderecos servicoEnderecos =
                new ServicoEnderecos(repositorioEnderecos, servicoLogradouros, servicoBairros, servicoCidades,
                                     servicoEstados);
            IServicoLojas servicoLojas = new ServicoLojas(repositorioLojas, servicoLojaUsuario, servicoUsuarios, servicoEnderecos);
            Usuario usuario = servicoUsuarios.GetPorId(1);
            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuario);
            UnitOfWorkNHibernate.GetInstancia().IniciarTransacao();
            servicoLojas.Salvar(loja, usuarios, usuario);
            UnitOfWorkNHibernate.GetInstancia().ConfirmarTransacao();
            
        }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            string server = ConfigurationManager.AppSettings["dbserver"];
            string username = ConfigurationManager.AppSettings["dbusername"];
            string password = ConfigurationManager.AppSettings["dbpassword"];
            _factory =
                Fluently.
                    Configure().CurrentSessionContext<ThreadLocalSessionContext>().
                    Database(MySQLConfiguration
                                 .Standard
                                 .ConnectionString(c => c.Server(server)
                                                         .Database("graxei")
                                                            .Username(username)
                                                            .Password(password)
                                 ).ShowSql()
                    ).
                    Mappings(m =>
                             m.FluentMappings.AddFromAssemblyOf<ProdutoMap>()
                              .Conventions.Add<ClasseComumConvencao>()).
                    BuildConfiguration().BuildSessionFactory();
            UnitOfWorkNHibernate.GetInstancia(_factory);

            ResetDatabase(_factory);
        }

        private static void ResetDatabase(ISessionFactory factory)
        {
            ISession session = _factory.GetCurrentSession();
            session.CreateSQLQuery("call graxei.cleanup_database()").ExecuteUpdate();
        }

        private static ISessionFactory _factory;
    }
}
