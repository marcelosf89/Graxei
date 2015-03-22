using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Negocio.Contrato.Especificacoes
{
    public class ResultadoEspecificacao
    {
        public ResultadoEspecificacao()
        {
            _ok = true;
        }

        public ResultadoEspecificacao(bool ok, string mensagem)
        {
            _ok = ok;
            _mensagem = mensagem;
        }

        private bool _ok;

        private string _mensagem = "Ok";
    }
}
