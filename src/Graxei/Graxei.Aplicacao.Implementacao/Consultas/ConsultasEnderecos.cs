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

        public List<Endereco> GetPorLoja(long idLoja)
        {
            return _servicoEnderecos.GetPorLoja(idLoja);
        }

        public Endereco Get(long id)
        {
            return _servicoEnderecos.Get(id);
        }

        private IServicoEnderecos _servicoEnderecos;

        #endregion

        
    }
}