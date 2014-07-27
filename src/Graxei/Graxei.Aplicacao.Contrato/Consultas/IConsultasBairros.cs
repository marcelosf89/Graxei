using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasBairros
    {
        Bairro Get(string nomeBairro, string nomeCidade, long idEstado);
        IList<Bairro> GetPorCidade(string nomeCidade, long idEstado);
        Bairro Get(long id);
    }
}
