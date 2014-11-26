using System.Web.Mvc;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Infraestutura.PaginacaoChain
{
    public interface IPaginacaoChain
    {
        MvcHtmlString Get();
        IPaginacaoChain ProximoElemento { get; }
        void SetProximoElemento(IPaginacaoChain paginacaoChain);
    }
}
