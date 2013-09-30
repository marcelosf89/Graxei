using FAST.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface ISalvarEntidade<T> where T : Entidade
    {
        void Salvar(T t);
    }
}
