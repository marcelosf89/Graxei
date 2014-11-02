using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasTiposTelefone
    {
        TipoTelefone Get(string tipo);
    }
}
