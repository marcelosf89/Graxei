using FAST.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IEntidadesSalvar<T> : IServicoEntidades<T> where T : Entidade
    {
        void Salvar(T t);
    }
}