using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoEnderecos : IEntidadesSalvar<Endereco>, IEntidadesExcluir<Endereco>
    {
        List<Endereco> Get(long idLoja);
    }
}
