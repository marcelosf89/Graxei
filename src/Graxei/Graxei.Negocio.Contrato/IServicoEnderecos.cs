using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoEnderecos : IEntidadesSalvar<Endereco>, IEntidadesExcluir<Endereco>
    {
        Endereco Get(long id);
        List<Endereco> GetPorLoja(long idLoja);
    }
}
