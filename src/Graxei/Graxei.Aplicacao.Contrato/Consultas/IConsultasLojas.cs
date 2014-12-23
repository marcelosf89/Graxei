using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasLojas
    {
        Loja Get(long id);
        Loja GetPorNome(string nome);
        LojaContrato GetComEnderecos(long id);
        Loja GetPorUrl(string lojaNome);

        byte[] GetLogo(int idLoja, string caminhoImagem);
        byte[] GetImageBackground(int idLoja, string caminhoImagem);
    }
}
