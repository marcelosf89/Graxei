using Graxei.Aplicacao.Contrato;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasLojas : IConsultasLojas
    {
        public ConsultasLojas(IServicoLojas servicoLojas, IServicoEnderecos servicoEnderecos, IServicoTelefones servicoTelefones)
        {
            ServicoLojas = servicoLojas;
            ServicoEnderecos = servicoEnderecos;
            ServicoTelefones = servicoTelefones;
        }

        #region Implementation of IConsultasLojas

        public IServicoLojas ServicoLojas { get; private set; }
        public IServicoEnderecos ServicoEnderecos { get; private set; }
        public IServicoTelefones ServicoTelefones { get; private set; }

        #endregion
    }
}