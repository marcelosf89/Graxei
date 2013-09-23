using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoBairros : IServicoEntidades<Bairro>
    {
        IList<Bairro> Get(Cidade cidade);
        IList<Bairro> GetPorCidade(int idCidade);
        Bairro Get(string nomeBairro, string nomeCidade, int idEstado);
        Bairro Get(string nomeBairro, string nomeCidade, Estado estado);
        Bairro Get(string nomeBairro, int idCidade);
        Bairro Get(string nomeBairro, Cidade cidade);
    }
}
