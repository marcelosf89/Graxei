namespace Graxei.Transversais.Comum.Excecoes
{
    public class ObjetoJaExisteException : OperacaoEntidadeException
    {
        public ObjetoJaExisteException(string mensagem): base(mensagem)
        {
        }
    }
}
