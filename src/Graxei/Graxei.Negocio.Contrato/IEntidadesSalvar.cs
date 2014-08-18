using FAST.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IEntidadesSalvar<T> : IServicoEntidades<T> where T : Entidade
    {
        void PreSalvar(T t);
        void PreAtualizar(T t);
        T Salvar(T t);
    }
}