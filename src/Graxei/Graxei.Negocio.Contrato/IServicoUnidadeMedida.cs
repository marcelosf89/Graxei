using Graxei.Modelo;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoUnidadeMedida : IServicoEntidades<UnidadeMedida>
    {
        void PreSalvar(UnidadeMedida unidade);
        void PreAtualizar(UnidadeMedida unidade);
    }
}
