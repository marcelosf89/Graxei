using Graxei.Modelo;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioTiposTelefone
    {
        TipoTelefone Get(string tipo);
    }
}
