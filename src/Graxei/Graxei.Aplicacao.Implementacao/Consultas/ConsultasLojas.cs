using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasLojas : IConsultasLojas
    {
        public ConsultasLojas(IServicoLojas servicoLojas, IServicoEnderecos servicoEnderecos, IServicoTelefones servicoTelefones)
        {
            _servicoLojas = servicoLojas;
            _servicoEnderecos = servicoEnderecos;
        }

        #region Implementação de IConsultasLojas

        private IServicoLojas _servicoLojas;
        private IServicoEnderecos _servicoEnderecos;

        public Loja Get(long id)
        {
            return _servicoLojas.GetPorId(id);
        }

        public Loja GetComEnderecos(long id)
        {
            return _servicoLojas.GetComEnderecos(id);
        }

        public Loja GetPorNome(string nome)
        {
            return _servicoLojas.Get(nome);
        }

        #endregion
    }
}