using FAST.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioSalvar<T>: IRepositorioEntidades<T> where T : Entidade
    {
        T Salvar(T t);
    }
}
