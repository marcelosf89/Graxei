using Graxei.Transversais.ContratosDeDados.Api.PesquisaProdutos;
using System.Threading.Tasks;

namespace Graxei.Aplicacao.Contrato.Transacionais
{
    public interface IPesquisaProduto
    {
        Task RegistrarAsync(HistoricoPesquisa historicoPesquisa);
        //void RegistrarAsync(HistoricoPesquisa historicoPesquisa);
    }
}
