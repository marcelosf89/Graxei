using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoLogradouros : IServicoEntidades<Logradouro>
    {
        IList<Logradouro> Get(Bairro bairro);
        IList<Logradouro> GetPorBairro(long idBairro);
        IList<Logradouro> GetPorBairro(string nomeBairro, string nomeCidade, long idEstado);
        Logradouro Get(string nomeLogradouro, string nomeBairro, long idCidade);
        Logradouro Get(string nomeLogradouro, string nomeBairro, Cidade cidade);
        Logradouro Get(string nomeLogradouro, long idBairro);
        Logradouro Get(string nomeLogradouro, Bairro bairro);
    }
}
