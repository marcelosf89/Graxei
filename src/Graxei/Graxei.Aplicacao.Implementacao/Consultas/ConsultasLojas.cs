using System.Collections.Generic;
using Graxei.Aplicacao.Contrato;
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

        public Loja Get(int id)
        {
            return _servicoLojas.GetPorId(id);
        }

        public IList<Endereco> EnderecosRepetidos(Loja loja)
        {
            return _servicoEnderecos.EnderecosRepetidos(loja.Enderecos);
        }

        #endregion
    }
}