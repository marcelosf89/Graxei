using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioEnderecos : IRepositorioExcluir<Endereco>, IRepositorioSalvar<Endereco>
    {
        IList<Endereco> Todos(Loja loja);
        IList<Endereco> Todos(long idLoja);
        bool ExisteNaLoja(Endereco endereco);
    }
}
