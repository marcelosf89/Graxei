using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface ISegurancaEndereco
    {
        bool PermitidoAlterar(Endereco endereco, Usuario usuario);
    }
}
