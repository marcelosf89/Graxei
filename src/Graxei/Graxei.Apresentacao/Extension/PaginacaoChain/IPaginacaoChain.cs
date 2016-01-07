using Graxei.Apresentacao.Extension.PaginacaoChain.LinkBuilderStrategy;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Extension.PaginacaoChain
{
    public interface IPaginacaoChain
    {
        ILinkBuilderStrategy LinkBuilderStrategy { get; }
        MvcHtmlString Get();
        IPaginacaoChain ProximoElemento { get; }
        void SetProximoElemento(IPaginacaoChain paginacaoChain);
    }
}
