using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioEnderecos : IRepositorioExcluir<Endereco>
    {
        IList<Endereco> Todos(Loja loja);
        IList<Endereco> Todos(long idLoja);
        bool ExisteNaLoja(Endereco endereco);
    }
}
