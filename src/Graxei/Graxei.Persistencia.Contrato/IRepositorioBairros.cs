using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioBairros : IRepositorioEntidades<Bairro>
    {
        IList<Bairro> Get(Cidade cidade);
        IList<Bairro> Get(int idCidade);
        Bairro Get(string nomeBairro, string nomeCidade, Estado estado);
        Bairro Get(string nomeBairro, string nomeCidade, int idEstado);
        Bairro Get(string nomeBairro, int idCidade);
        Bairro Get(string nomeBairro, Cidade cidade);
    }
}
