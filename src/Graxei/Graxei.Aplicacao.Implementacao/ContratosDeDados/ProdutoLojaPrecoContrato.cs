using Graxei.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Aplicacao.Implementacao.ContratosDeDados
{
    public class ProdutoLojaPrecoContrato
    {
        public long IdProduto { get; set; }
        public long IdMeuProduto { get; set; }
        public string MinhaDescricao { get; set; }
        public long IdEndereco { get; set; }
        public double Preco { get; set; }
    }
}
 