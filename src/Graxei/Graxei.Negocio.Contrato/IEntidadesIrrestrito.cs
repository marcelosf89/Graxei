using FAST.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IEntidadesIrrestrito<T> : IEntidadesSalvar<T>, IEntidadesExcluir<T> where T : Entidade
    {
    }
}
