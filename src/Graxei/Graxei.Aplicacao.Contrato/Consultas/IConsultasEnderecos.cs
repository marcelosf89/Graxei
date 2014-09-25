using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasEnderecos
    {
        List<Endereco> Get(long idLoja);
    }
}
