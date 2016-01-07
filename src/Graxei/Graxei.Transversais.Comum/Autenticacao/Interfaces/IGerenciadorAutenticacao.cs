using Graxei.Modelo;

namespace Graxei.Transversais.Comum.Autenticacao.Interfaces
{
    public interface IGerenciadorAutenticacao
    {
        void Registrar(Usuario usuario);
        Usuario Get();
    }
}
