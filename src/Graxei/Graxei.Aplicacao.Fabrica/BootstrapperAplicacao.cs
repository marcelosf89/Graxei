using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Operacoes;
using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Contrato.TransformacaoDados;
using Graxei.Aplicacao.Implementacao;
using Graxei.Aplicacao.Implementacao.Consultas;
using Graxei.Aplicacao.Implementacao.Operacoes;
using Graxei.Aplicacao.Implementacao.Transacionais;
using Graxei.Modelo;
using Graxei.Negocio.Fabrica;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Comum.TransformacaoDados.Interface;
using Microsoft.Practices.Unity;
using System.Net.Http;

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
                .RegisterType<IConsultasBairros, ConsultaBairros>()
                .RegisterType<IConsultaCidades, ConsultaCidades>()
                .RegisterType<IConsultaEstados, ConsultaEstados>()
                .RegisterType<IConsultasLogradouros, ConsultaLogradouros>()
                .RegisterType<IConsultaLogin, ConsultaLogin>()
                .RegisterType<IConsultasLojas, ConsultasLojas>()
                .RegisterType<IConsultasUsuarios, ConsultasUsuarios>()
                .RegisterType<IConsultaEnderecos, ConsultaEnderecos>()
                .RegisterType<ITransformacaoMutua<Loja, LojaContrato>, LojasTransformacao>()
                .RegisterType<IConsultaFabricantes,ConsultaFabricantes>()
                .RegisterType<IConsultasProdutoVendedor, ConsultasProdutoVendedor>()
                .RegisterType<IConsultasTiposTelefone, ConsultasTiposTelefone>()
                .RegisterType<IConsultasListaLojas, ConsultasListaLojas>()
                .RegisterType<IConsultasPlanos, ConsultasPlanos>()
                .RegisterType<IConsultasProdutos, ConsultasProdutos>()
                .RegisterType<IConsultaListaProdutosLoja, ConsultaListaProdutosLoja>()
                .RegisterType<IGerenciamentoMensageria, GerenciamentoMensageria>()
                .RegisterType<IOperacaoEndereco, OperacaoEndereco>()
                .RegisterType<IPesquisaProduto, PesquisaProduto>()
                .RegisterType<HttpClient, HttpClient>(new InjectionConstructor());
        }
    }
}