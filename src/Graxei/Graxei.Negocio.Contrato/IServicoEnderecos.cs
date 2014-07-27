using System.Collections.Generic;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoEnderecos : IEntidadesExcluir<Endereco>
    {
        IList<Endereco> Todos(Loja loja);
        IList<Endereco> Todos(long idLoja);
        void PreSalvar(Endereco endereco);
        void PreAtualizar(Endereco endereco);
        void ChecarRepetidosAoSalvar(Endereco endereco);
        void ChecarRepetidosAoAtualizar(Endereco endereco);
    }
}
