using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;
using Microsoft.Practices.Unity;
namespace Graxei.Negocio.Fabrica
{
    public class ServicoFabricaUnity : IServicosFabrica
    {

        public ServicoFabricaUnity(IUnityContainer unity)
        {
            _unityContainer = unity;
        }

        public IServicoUsuarios GetServicoProdutos()
        {
            return _unityContainer.Resolve<IServicoUsuarios>();
        }

        private readonly IUnityContainer _unityContainer;
    }
}
