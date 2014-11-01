using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.Utilidades.TransformacaoDados.Interface;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasLojas : IConsultasLojas
    {
        public ConsultasLojas(IServicoLojas servicoLojas, IServicoEnderecos servicoEnderecos, IServicoTelefones servicoTelefones, ITransformacaoMutua<Loja, LojaContrato> transformacao)
        {
            _servicoLojas = servicoLojas;
            _servicoEnderecos = servicoEnderecos;
            _transformacao = transformacao;
        }

        #region Implementação de IConsultasLojas

        private IServicoLojas _servicoLojas;
        private IServicoEnderecos _servicoEnderecos;
        private ITransformacaoMutua<Loja, LojaContrato> _transformacao;

        public Loja Get(long id)
        {
            return _servicoLojas.GetPorId(id);
        }

        public LojaContrato GetComEnderecos(long id)
        {
            Loja loja = _servicoLojas.GetComEnderecos(id);
            return _transformacao.Transformar(loja);
        }

        public Loja GetPorNome(string nome)
        {
            return _servicoLojas.Get(nome);
        }

        #endregion
    }
}