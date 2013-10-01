using FAST.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IExcluirEntidade<T> : IServicoEntidades<T> where T : Entidade
    {
        void Excluir(T t);
    }
}
