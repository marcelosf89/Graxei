using Graxei.Modelo;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoLojaUsuario : IServicoEntidades<LojaUsuario>
    {
        bool Existe(Loja loja, Usuario usuario);
    }
}
