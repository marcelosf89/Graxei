using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{ 
    public interface IServicoUsuarios : IServicoEntidades<Usuario>
    {
        Usuario GetPorLogin(string login);
        Usuario GetPorNome(string nome);
        Usuario GetPorEmail(string email);
        Usuario AutenticarPorLogin(string login, string senha);
        Usuario AutenticarPorEmail(string email, string senha);
    }
}
