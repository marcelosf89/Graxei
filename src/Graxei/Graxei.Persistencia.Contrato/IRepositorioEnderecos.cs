using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioEnderecos : IRepositorioExcluir<Endereco>, IRepositorioSalvar<Endereco>
    {
        IList<Endereco> Todos(Loja loja);
        IList<Endereco> Todos(long idLoja);
        Endereco Get(long idLoja, string logradouro, string numero, string complemento, long idBairro);
        List<Endereco> GetPorLoja(long idLoja);
        Endereco Get(long idEndereco);
    }
}
