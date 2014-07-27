using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Aplicacao.Contrato.Transacionais
{
    public interface IGerenciamentoLojas : ITransacional
    {
        LojaContrato SalvarLoja(LojaContrato loja, Usuario usuario);
        LojaContrato SalvarLoja(LojaContrato loja);
        void ExcluirLoja(LojaContrato loja);
    }

}