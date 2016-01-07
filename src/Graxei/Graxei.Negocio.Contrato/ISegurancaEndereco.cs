using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface ISegurancaEndereco
    {
        bool PermitidoAlterar(Endereco endereco, Usuario usuario);
    }
}
