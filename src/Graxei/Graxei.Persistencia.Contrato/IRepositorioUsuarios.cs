using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioUsuarios : IRepositorioEntidades<Usuario>
    {
        Usuario GetPorLogin(string login);
        Usuario GetPorNome(string nome);
        Usuario GetPorEmail(string email);
    }
}
