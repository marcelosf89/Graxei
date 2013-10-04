using FAST.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IEntidadesExcluir<T> : IServicoEntidades<T> where T : Entidade
    {
        void Excluir(T t);
    }
}