namespace Graxei.Negocio.Contrato.Especificacoes
{
    public interface IEspecificacao<T> where T : class
    {
        ResultadoEspecificacao Satisfeita(T t);
    }
}
