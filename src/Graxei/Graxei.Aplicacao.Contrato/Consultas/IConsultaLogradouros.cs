using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasLogradouros
    {
        IList<Logradouro> Get(string nomeBairro, string nomeCidade, long idEstado);
    }
}
