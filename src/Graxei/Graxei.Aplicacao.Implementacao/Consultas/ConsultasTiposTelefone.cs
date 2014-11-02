using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasTiposTelefone : IConsultasTiposTelefone
    {
        public ConsultasTiposTelefone(IServicoTiposTelefone servicoTiposTelefone)
        {
            _servicoTiposTelefone = servicoTiposTelefone;
        }

        public TipoTelefone Get(string tipo)
        {
            return _servicoTiposTelefone.Get(tipo);
        }

        private IServicoTiposTelefone _servicoTiposTelefone;
    }
}