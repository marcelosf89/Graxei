using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioTelefones : IRepositorioExcluir<Telefone>
    {
        IList<Telefone> Todos(long idEndereco);
    }
}
