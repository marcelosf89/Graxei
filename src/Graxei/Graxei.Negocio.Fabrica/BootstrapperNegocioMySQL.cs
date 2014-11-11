using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Graxei.Persistencia.Contrato;
using Graxei.Persistencia.Implementacao.NHibernate;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Fabrica
{
    public class BootstrapperNegocioMySQL
    {
        public static void Register(IUnityContainer container)
        {
            // Usuários
            container.RegisterType<IRepositorioUsuarios, UsuariosNHibernateMySQL>()
                 .RegisterType<IServicoUsuarios, ServicoUsuarios>();

            // Lojas
            container.RegisterType<IRepositorioLojas, LojasNHibernateMySQL>()
                       .RegisterType<IServicoLojas, ServicoLojas>();

            // Produtos
            container.RegisterType<IRepositorioProdutos, ProdutosNHibernateMySQL>()
                     .RegisterType<IServicoProdutos, ServicoProdutos>();

            // ProdutosVendedores
            container.RegisterType<IRepositorioProdutoVendedor, ProdutoVendedorNHibernateMySQL>()
                  .RegisterType<IServicoProdutoVendedor, ServicoProdutoVendedor>();

            // Atributos
            container.RegisterType<IRepositorioAtributos, AtributosNHibernateMySQL>()
               .RegisterType<IServicoAtributos, ServicoAtributos>();

            // Fabricantes
            container.RegisterType<IRepositorioFabricantes, FabricantesNHibernateMySQL>()
                       .RegisterType<IServicoFabricantes, ServicoFabricantes>();

            // Estados
            container.RegisterType<IRepositorioEstados, EstadosNHibernateMySQL>()
                                 .RegisterType<IServicoEstados, ServicoEstados>();

            // Cidades
            container.RegisterType<IRepositorioCidades, CidadesNHibernateMySQL>()
                       .RegisterType<IServicoCidades, ServicoCidades>();

            // Bairros
            container.RegisterType<IRepositorioBairros, BairrosNHibernateMySQL>()
                       .RegisterType<IServicoBairros, ServicoBairros>();

            // Logradouros
            container.RegisterType<IRepositorioLogradouros, LogradourosNHibernateMySQL>()
                       .RegisterType<IServicoLogradouros, ServicoLogradouros>();

            // Telefones
            container.RegisterType<IRepositorioTelefones, TelefonesNHibernateMySQL>()
                  .RegisterType<IServicoTelefones, ServicoTelefones>();

            // Endereços
            container.RegisterType<IRepositorioEnderecos, EnderecosNHibernateMySQL>()
                       .RegisterType<IServicoEnderecos, ServicoEnderecos>();

            // Telefone
            container.RegisterType<IRepositorioTiposTelefone, TiposTelefoneNHibernateMySQL>()
                       .RegisterType<IServicoTiposTelefone, ServicoTiposTelefone>();

            //Lista lojas
            container.RegisterType<IRepositorioListaLojas, ListaLojasNHibernateMySQL>()
                       .RegisterType<IServicoListaLojas, ServicoListaLojas>();
        }
    }
}
