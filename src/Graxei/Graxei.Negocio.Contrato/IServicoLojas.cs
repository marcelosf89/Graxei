using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoLojas : IEntidadesIrrestrito<Loja>
    {
        Loja Get(string nome);
        Loja Salvar(Loja loja, Usuario usuario);
        Loja GetComEnderecos(long id);
        Loja GetPorUrl(string nome);
        IList<long> GetIdsEnderecos(long idLoja);
    }
}