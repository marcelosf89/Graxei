using System.Collections.Generic;
using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasEnderecos : IConsultasEnderecos
    {

        #region Construtor
        public ConsultasEnderecos(IServicoEnderecos servicoEnderecos)
        {
            _servicoEnderecos = servicoEnderecos;
        }
        #endregion

        #region Implementação de IConsultasEnderecos

        public List<Endereco> Get(long idLoja)
        {
            return _servicoEnderecos.Get(idLoja);
        }

        public IServicoEnderecos ServicoEnderecos { get; private set; }

        private IServicoEnderecos _servicoEnderecos;

        #endregion

        
    }
}