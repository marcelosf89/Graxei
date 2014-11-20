using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Contrato.TransformacaoDados;
using Graxei.Aplicacao.Implementacao;
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

            container.RegisterType<IGerenciamentoLojas, GerenciamentoLojas>()
                .RegisterType<IGerenciamentoProdutos, GerenciamentoProdutos>()
                .RegisterType<IGerenciamentoEnderecos, GerenciamentoEnderecos>()
                .RegisterType<IConsultasBairros, ConsultasBairros>()
                .RegisterType<IConsultasCidades, ConsultasCidades>()
                .RegisterType<IConsultasEstados, ConsultasEstados>()
                .RegisterType<IConsultasLogradouros, ConsultasLogradouros>()
                .RegisterType<IConsultasLogin, ConsultasLogin>()
                .RegisterType<IConsultasLojas, ConsultasLojas>()
                .RegisterType<IConsultasUsuarios, ConsultasUsuarios>()
                .RegisterType<IConsultasEnderecos, ConsultasEnderecos>()
                .RegisterType<ITransformacaoMutua<Loja, LojaContrato>, LojasTransformacao>()
                .RegisterType<IConsultasFabricantes,ConsultasFabricantes>()
                .RegisterType<IConsultasProdutoVendedor, ConsultasProdutoVendedor>()
                .RegisterType<IConsultasTiposTelefone, ConsultasTiposTelefone>()
                .RegisterType<IConsultasListaLojas, ConsultasListaLojas>()
                .RegisterType<IConsultasPlanos, ConsultasPlanos>()
                .RegisterType<IConsultasProdutos, ConsultasProdutos>();
        }
    }
}