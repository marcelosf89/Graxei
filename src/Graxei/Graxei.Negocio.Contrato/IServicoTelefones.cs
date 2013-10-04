using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoTelefones : IEntidadesExcluir<Telefone>
    {
        IList<Telefone> Todos(long idEndereco);
    }
}
