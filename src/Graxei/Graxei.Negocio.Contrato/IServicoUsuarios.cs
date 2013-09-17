using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{ 
    public interface IServicoUsuarios : IServicoEntidades<Usuario>
    {
        Usuario GetPorLogin(string login);
        Usuario GetPorNome(string nome);
        Usuario GetPorEmail(string email);
    }
}
