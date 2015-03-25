using Graxei.Modelo.Generico;
namespace Graxei.Negocio.Contrato
{
    public interface IEntidadesSalvar<T> : IServicoEntidades<T> where T : Entidade
    {
        T Salvar(T t);
    }
}