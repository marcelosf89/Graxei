using FAST.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IExcluirEntidade<T> where T : Entidade
    {
        void Excluir(T t);
    }
}
