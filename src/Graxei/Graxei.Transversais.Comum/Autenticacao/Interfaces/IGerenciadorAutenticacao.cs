using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graxei.Modelo;

namespace Graxei.Transversais.Comum.Autenticacao.Interfaces
{
    public interface IGerenciadorAutenticacao
    {
        void Registrar(Usuario usuario);
        Usuario Get();
    }
}
