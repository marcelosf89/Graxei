namespace Graxei.Transversais.Comum.LogAplicacao
{
    public interface ILogAplicacao
    {
        void Registrar(string mensagem);
        void Registrar(string escopo, string mensagem);
    }
}
