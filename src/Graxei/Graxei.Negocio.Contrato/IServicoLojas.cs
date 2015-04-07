using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoLojas : IEntidadesIrrestrito<Loja>
    {
        Loja Get(string nome);

        Loja Salvar(Loja loja, Usuario usuario);

        Loja GetComEnderecos(long id);

        Loja GetComEnderecosPlanos(long id);

        Loja GetPorUrl(string nome);

        Endereco GetEnderecoComTelefones(long idEndereco);

        IList<long> GetIdsEnderecos(long idLoja);

        bool UsuarioAtualAssociado(Loja loja);

        Plano GetPlano(long idLoja);
    }
}