using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoBairros : IEntidadesIrrestrito<Bairro>
    {
        IList<Bairro> Get(Cidade cidade);
        IList<Bairro> GetPorCidade(long idCidade);
        IList<Bairro> GetPorCidade(string nomeCidade, long idEstado);
        Bairro Get(string nomeBairro, string nomeCidade, long idEstado);
        Bairro Get(string nomeBairro, string nomeCidade, Estado estado);
        Bairro Get(string nomeBairro, long idCidade);
        Bairro Get(string nomeBairro, Cidade cidade);
    }
}
