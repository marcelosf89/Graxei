using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Aplicacao.Contrato.Operacoes;
using Graxei.Modelo;
using Graxei.Negocio.Contrato.Factories;
using Graxei.Transversais.ContratosDeDados;
namespace Graxei.Aplicacao.Implementacao.Operacoes
{
    public class OperacaoEndereco : IOperacaoEndereco
    {
        public OperacaoEndereco(IEnderecosBuilder enderecosBuilder, IConsultasBairros consultaBairros)
        {
            _enderecosBuilder = enderecosBuilder;
            _consultaBairros = consultaBairros;
        }

        public Endereco GetComBaseEm(EnderecoVistaContrato enderecoVistaContrato)
        {
            Bairro bairro = _consultaBairros.Get(enderecoVistaContrato.Bairro, enderecoVistaContrato.Cidade, enderecoVistaContrato.IdEstado);
            Endereco endereco = _enderecosBuilder.SetLogradouro(enderecoVistaContrato.Logradouro)
                                                 .SetComplemento(enderecoVistaContrato.Complemento)
                                                 .SetNumero(enderecoVistaContrato.Numero)
                                                 .SetBairro(bairro).Build();
            return endereco;
        }

        private IEnderecosBuilder _enderecosBuilder;
            
        private IConsultasBairros _consultaBairros;
    }
}
