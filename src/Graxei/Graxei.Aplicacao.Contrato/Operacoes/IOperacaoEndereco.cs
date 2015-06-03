using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Aplicacao.Contrato.Operacoes
{
    public interface IOperacaoEndereco
    {
        Endereco GetComBaseEm(EnderecoVistaContrato enderecoVistaContrato);
    }
}
