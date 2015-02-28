using Graxei.Modelo.Generico;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioExcluir<T>: IRepositorioEntidades<T> where T : Entidade
    {
        void Excluir(T t);
    }
}
