using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Implementacao.Consultas;
using Graxei.Aplicacao.Implementacao.Transacionais;
using Graxei.Negocio.Fabrica;
using Microsoft.Practices.Unity;

namespace Graxei.Aplicacao.Fabrica
{
    public static class BootstrapperAplicacao
    {

        public static void RegisterTypes(IUnityContainer container)
        {
            BootstrapperNegocio.RegisterTypes(container);

            // Lojas - Transacional
            container.RegisterType<IGerenciamentoLojas, GerenciamentoLojas>(
                /*new InjectionFactory(
                    p =>
                    new GerenciamentoLojas(container.Resolve<IServicoLojas>(), container.Resolve<IServicoEnderecos>(),
                                           container.Resolve<IServicoTelefones>(), container.Resolve<IServicoUsuarios>()))*/
                );

            // Produtos Vendedor - Transacional
            container.RegisterType<IGerenciamentoProdutos, GerenciamentoProdutos>();

            // Lojas - Consultas
            container.RegisterType<IConsultasLojas, ConsultasLojas>(
               /* new InjectionFactory(
                    p =>
                    new GerenciamentoLojas(container.Resolve<IServicoLojas>(), container.Resolve<IServicoEnderecos>(),
                                           container.Resolve<IServicoTelefones>(), container.Resolve<IServicoUsuarios>()))*/
               );
            
            // Usuários - Consultas
            container.RegisterType<IConsultasUsuarios, ConsultasUsuarios>(
                /*new InjectionFactory(p => new ConsultasUsuarios(container.Resolve<IServicoUsuarios>()))*/
                );

            // Endereços - Consultas
            container.RegisterType<IConsultasEnderecos, ConsultasEnderecos>(
                /*new InjectionFactory(p => new ConsultasEnderecos(container.Resolve<IServicoEnderecos>()))*/
                );

            // Login - Consultas
            container.RegisterType<IConsultasLogin, ConsultasLogin>();
        }
    }
}