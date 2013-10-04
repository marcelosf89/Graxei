using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioLogradouros : IRepositorioIrrestrito<Logradouro>
    {
        IList<Logradouro> Get(Bairro bairro);
        IList<Logradouro> Get(long idBairro);
        IList<Logradouro> GetPorBairro(string nomeBairro, string nomeCidade, long idEstado);
        Logradouro Get(string nomeLogradouro, string nomeBairro, Cidade cidade);
        Logradouro Get(string nomeLogradouro, string nomeBairro, long idCidade);
        Logradouro Get(string nomeLogradouro, long idBairro);
        Logradouro Get(string nomeLogradouro, Bairro bairro);
    }
}
