using FAST.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioExcluir<T> where T : Entidade
    {
        void Excluir(T t);
    }
}
