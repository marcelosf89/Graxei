using System.Collections.Generic;

namespace Graxei.Apresentacao.Extension.PaginacaoChain.LinkBuilderStrategy
{
    public interface ILinkBuilderStrategy
    {
        List<string> Build(long de, long ate);
        List<string> SubstituirElementoAtual(long elemento);
    }
}
