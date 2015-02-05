
using Graxei.Modelo;
namespace Graxei.Aplicacao.Contrato.Operacoes
{
    public interface IGerenciamentoMensageria
    {
        void Enviar(Mensagem mensagem, ConfiguracaoMail configuracao);
    }
}