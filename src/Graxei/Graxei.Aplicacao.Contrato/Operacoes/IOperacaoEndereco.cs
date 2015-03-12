using Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models;
using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Operacoes
{
    public interface IOperacaoEndereco
    {
        Endereco GetComBaseEm(EnderecoVistaContrato enderecoVistaContrato);
    }
}
