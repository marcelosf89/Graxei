using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Extension.PaginacaoChain
{
    public interface IPaginacaoChain
    {
        MvcHtmlString Get();
        IPaginacaoChain ProximoElemento { get; }
        void SetProximoElemento(IPaginacaoChain paginacaoChain);
    }
}
