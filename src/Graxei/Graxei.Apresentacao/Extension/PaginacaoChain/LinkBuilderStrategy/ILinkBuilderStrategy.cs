using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Apresentacao.Extension.PaginacaoChain.LinkBuilderStrategy
{
    public interface ILinkBuilderStrategy
    {
        List<string> Build(long de, long ate);
        List<string> SubstituirElementoAtual(long elemento);
    }
}
