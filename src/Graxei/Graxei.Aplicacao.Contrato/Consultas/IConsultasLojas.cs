using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasLojas
    {
        Loja Get(long id);
        Loja GetPorNome(string nome);
        LojaContrato GetComEnderecos(long id);
        Loja GetPorUrl(string lojaNome);
        long GetIdDoUnicoEndereco(long idLoja);

        byte[] GetLogo(int idLoja, string caminhoImagem);
        byte[] GetImageBackground(int idLoja, string caminhoImagem);
    }
}
