using Graxei.Negocio.Contrato;
using Microsoft.Practices.Unity;
namespace Graxei.Negocio.Fabrica
{
    public class ServicoFabricaUnity : IServicosFabrica
    {

        public ServicoFabricaUnity(IUnityContainer unity)
        {
            _unityContainer = unity;
        }

        public IServicoProdutos GetServicoProdutos()
        {
            return _unityContainer.Resolve<IServicoProdutos>();
        }

        private readonly IUnityContainer _unityContainer;
    }
}
