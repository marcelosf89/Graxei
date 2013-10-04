using Graxei.Aplicacao.Contrato.Transacionais;
using Graxei.Aplicacao.Fabrica;
using Graxei.Aplicacao.Implementacao.Transacionais;
using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Microsoft.Practices.Unity;

namespace TestesUnity.ConfiguracoesUnity
{
    public class ContainerGraxei
    {
        public static void RegisterTypes(IUnityContainer container)
        {
           BootstrapperAplicacao.RegisterTypes(container);
            container.RegisterType<IGerenciamentoLojas, GerenciamentoLojas>(
                new InjectionFactory(
                    p =>
                    new GerenciamentoLojas(container.Resolve<ServicoLojas>(), container.Resolve<ServicoEnderecos>(),
                                           container.Resolve<IServicoTelefones>(), container.Resolve<IServicoUsuarios>())));
        }
    }
}
