using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Contrato.TransformacaoDados;
using Graxei.Aplicacao.Implementacao.Consultas;
using Graxei.Aplicacao.Implementacao.Transacionais;
using Graxei.Modelo;
using Graxei.Negocio.Fabrica;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;
using Microsoft.Practices.Unity;

namespace Graxei.Aplicacao.Fabrica
{
    public static class BootstrapperAplicacao
    {

        public static void RegisterTypes(IUnityContainer container)
        {
            BootstrapperNegocio.RegisterTypes(container);

            // Lojas - Transacional
            container.RegisterType<IGerenciamentoLojas, GerenciamentoLojas>();

            // Produtos Vendedor - Transacional
            container.RegisterType<IGerenciamentoProdutos, GerenciamentoProdutos>();

            // Lojas - Consultas
            container.RegisterType<IConsultasLojas, ConsultasLojas>();
            
            // Usuários - Consultas
            container.RegisterType<IConsultasUsuarios, ConsultasUsuarios>();

            // Endereços - Consultas
            container.RegisterType<IConsultasEnderecos, ConsultasEnderecos>();

            // Login - Consultas
            container.RegisterType<IConsultasLogin, ConsultasLogin>();

            container.RegisterType<ITransformacaoMutua<Loja, LojaContrato>, LojasTransformacao>();

        }
    }
}