using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Aplicacao.Contrato.Transacionais
{
    public interface IGerenciamentoEnderecos : ITransacional
    {
        Endereco Salvar(EnderecoVistaContrato endereco);

        void Remover(Endereco endereco);
    }
}
