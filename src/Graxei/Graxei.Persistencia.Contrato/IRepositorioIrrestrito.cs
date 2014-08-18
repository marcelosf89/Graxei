using FAST.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioIrrestrito<T> : IRepositorioSalvar<T>, IRepositorioExcluir<T> where T : Entidade
    {
    }
}
