using Graxei.Transversais.ContratosDeDados.TinyTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Teste.Models
{
    public class PesquisaProdutoLojaModel
    {
        public long IdLoja { get; set; }
        
        public string DescricaoProduto { get; set; }
        
        public bool MeusProdutos { get; set; }

        public long PaginaAtualLista { get; set; }

        public long TotalElementosLista { get; set; }
    }
}