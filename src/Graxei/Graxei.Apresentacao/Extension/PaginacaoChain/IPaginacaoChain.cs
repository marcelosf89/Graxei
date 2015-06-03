using Graxei.Apresentacao.Extension.PaginacaoChain.LinkBuilderStrategy;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

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
