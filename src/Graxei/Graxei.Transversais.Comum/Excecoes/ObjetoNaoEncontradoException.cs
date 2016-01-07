namespace Graxei.Transversais.Comum.Excecoes
{
    public class ObjetoNaoEncontradoException : OperacaoEntidadeException
    {
        public ObjetoNaoEncontradoException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
