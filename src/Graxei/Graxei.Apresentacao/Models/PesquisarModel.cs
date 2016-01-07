using Graxei.Transversais.ContratosDeDados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graxei.Apresentacao.Models
{
    public class PesquisarModel
    {
        public long? NumeroMaximoPagina { get; set; }
        
        public long PaginaSelecionada { get; set; }
        
        public String Texto { get; set; }

        public long TempoBusca { get; set; }

        public IList<PesquisaContrato> PesquisaContrato { get; set; }

        public string PartialRender
        {
            get
            {
                if (PesquisaContrato == null || !PesquisaContrato.Any())
                {
                    return "SemResultado";
                }

                return "ComResultado";
            }
        }

    }
}