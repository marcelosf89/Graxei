﻿using System;
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

        public bool Ok
        {
            get
            {
                return _ok;
            }
        }

        public string Mensagem
        {
            get
            {
                return _mensagem;
            }
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
