using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoLojas : IEntidadesIrrestrito<Loja>
    {
        Loja Get(string nome);
        Loja Salvar(Loja loja, Usuario usuario);
        Loja GetComEnderecos(long id);
    }
}