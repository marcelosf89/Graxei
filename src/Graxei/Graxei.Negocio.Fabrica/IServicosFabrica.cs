using Graxei.Negocio.Contrato;
using Graxei.Negocio.Implementacao;

namespace Graxei.Negocio.Fabrica
{
    public interface IServicosFabrica
    {
        IServicoProdutos GetServicoProdutos();
    }
}
