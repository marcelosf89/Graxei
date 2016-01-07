using Graxei.Negocio.Contrato.Especificacoes;

namespace Graxei.Negocio.Implementacao.Teste.Especificacoes
{
    public class SpecCommon
    {
        public static bool ResultadoEspecificacaoNotOk(ResultadoEspecificacao resultadoEspecificacao, string mensagem)
        {
            return (!resultadoEspecificacao.Ok && resultadoEspecificacao.Mensagem == mensagem);
        }
    }
}
