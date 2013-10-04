namespace Graxei.Aplicacao.Contrato
{
    public interface ITransacional
    {
        void IniciarTransacao();
        void Confirmar();
        void Desfazer();
    }
}
