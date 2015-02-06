using Graxei.Transversais.ContratosDeDados.TinyTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Transversais.ContratosDeDados
{
    public class PesquisaProdutoContrato
    {
        public long IdLoja { get; set; }

        public long IdUnicoEndereco { get; set; }

        public string DescricaoProduto { get; set; }
        
        public bool MeusProdutos { get; set; }

        public int PaginaAtualLista { get; set; }

        public long TotalElementosLista { get; set; }
    }
}