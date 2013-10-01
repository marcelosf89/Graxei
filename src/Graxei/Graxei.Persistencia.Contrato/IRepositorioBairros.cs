using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioBairros : IRepositorioIrrestrito<Bairro>
    {
        IList<Bairro> Get(Cidade cidade);
        IList<Bairro> Get(long idCidade);
        IList<Bairro> GetPorCidade(string nomeCidade, long idEstado);
        Bairro Get(string nomeBairro, string nomeCidade, Estado estado);
        Bairro Get(string nomeBairro, string nomeCidade, long idEstado);
        Bairro Get(string nomeBairro, long idCidade);
        Bairro Get(string nomeBairro, Cidade cidade);
    }
}
