using Graxei.Negocio.Contrato.Especificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
