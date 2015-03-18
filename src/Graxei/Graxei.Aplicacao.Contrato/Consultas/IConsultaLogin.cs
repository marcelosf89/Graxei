using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultaLogin
    {
        Usuario AutenticarPorLogin(string login, string senha);
        Usuario GetPorNome(string nome);
        IServicoUsuarios ServicoUsuarios { get; }
    }
}