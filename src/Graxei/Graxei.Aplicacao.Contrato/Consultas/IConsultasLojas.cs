using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using System.Collections.Generic;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasLojas
    {
        Loja Get(long id);

        Loja GetPorNome(string nome);

        Loja GetComEnderecosPlanos(long id);

        LojaContrato GetComEnderecos(long id);

        Loja GetPorUrl(string lojaNome);

        Plano GetPlano(long idLoja);

        long GetIdDoUnicoEndereco(long idLoja);

        Endereco GetEnderecoComTelefones(long idEndereco);

        byte[] GetLogo(int idLoja, string caminhoImagem);

        byte[] GetImageBackground(int idLoja, string caminhoImagem);
    }
}
