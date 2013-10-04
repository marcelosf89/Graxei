using Graxei.Modelo;
namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioLojaUsuario : IRepositorioEntidades<LojaUsuario>
    {
        bool Existe(Loja loja, Usuario usuario);
    }
}
