using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoLojas : IEntidadesIrrestrito<Loja>
    {
        Loja Get(string nome);
        void Salvar(Loja loja, IList<Usuario> usuarios, Usuario usuarioLog);
        IServicoEnderecos ServicoEnderecos { get; }
    }
}