using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultaEnderecos
    {
        List<Endereco> GetPorLoja(long idLoja);
        Endereco Get(long id);
    }
}
