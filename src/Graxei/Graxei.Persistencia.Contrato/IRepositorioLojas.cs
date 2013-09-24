using Graxei.Modelo;
namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioLojas : IRepositorioEntidades<Loja>
    {
        Loja Get(string nome);
    }
}
