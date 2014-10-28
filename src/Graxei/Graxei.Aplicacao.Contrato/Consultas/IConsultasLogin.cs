using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasLogin
    {
        Usuario AutenticarPorLogin(string login, string senha);
        Usuario AutenticarPorEmail(string email, string senha);
        IServicoUsuarios ServicoUsuarios { get;  }

        Usuario GetPorEmail(string email);
    }
}