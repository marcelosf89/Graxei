using Graxei.Transversais.ContratosDeDados.TinyTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Apresentacao.MVC4Unity.Areas.Teste.Models
{
    public class PesquisaProdutoLojaModel
    {
        private TotalElementosLista _totalElementosLista;

        private PaginaAtualLista _paginaAtualLista;

        public long IdLoja { get; set; }
        
        public string DescricaoProduto { get; set; }
        
        public bool MeusProdutos { get; set; }

        public PaginaAtualLista PaginaAtualLista
        {
            get
            {
                if (_paginaAtualLista == null)
                {
                    _paginaAtualLista = new PaginaAtualLista(1);
                }
                return _paginaAtualLista;
            }
            set
            {
                _paginaAtualLista = value;
            }
        }

        public TotalElementosLista TotalElementosLista
        {
            get
            {
                if (_totalElementosLista == null)
                {
                    _totalElementosLista = new TotalElementosLista(1);
                }
                return _totalElementosLista;
            }
            set
            {
                _totalElementosLista = value;
            }
        }
    }
}