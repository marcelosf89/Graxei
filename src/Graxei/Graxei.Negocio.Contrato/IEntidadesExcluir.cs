using FAST.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IEntidadesExcluir<T> : IServicoEntidades<T> where T : Entidade
    {
        void PreExcluir(T t);
        void Excluir(T t);
    }
}