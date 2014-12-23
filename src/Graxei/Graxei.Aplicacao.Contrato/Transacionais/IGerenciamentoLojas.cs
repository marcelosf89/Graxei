using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Aplicacao.Contrato.Transacionais
{
    public interface IGerenciamentoLojas : ITransacional
    {
        LojaContrato Salvar(LojaContrato loja, Usuario usuario);
        LojaContrato Salvar(LojaContrato loja);
        void ExcluirLoja(LojaContrato loja);
        void AdicionarLogo(Loja loja, string caminhoImagem, byte[] arquivo, string extensao);

        void AtualizarUrl(Loja loja);

        void AdicionarBackground(Loja loja, string caminhoImagem, byte[] arquivo, string extensao);
    }

}