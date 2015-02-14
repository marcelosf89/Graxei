using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graxei.Transversais.ContratosDeDados
{
    public class ProdutoLojaPrecoContrato
    {
        public long IdProduto { get; set; }
        public long IdMeuProduto { get; set; }
        public string MinhaDescricao { get; set; }
        public long IdEndereco { get; set; }
        public double Preco { get; set; }

        public OperacaoProdutoLoja OperacaoNoContrato
        {
            get 
            {
                if (Preco <= 0)
                {
                    return OperacaoProdutoLoja.Excluir;
                }
                else if (IdMeuProduto <= 0 && IdProduto > 0 && IdEndereco > 0)
                {
                    return OperacaoProdutoLoja.Incluir;
                }
                else if (IdMeuProduto > 0)
                {
                    return OperacaoProdutoLoja.Alterar;
                }

                return OperacaoProdutoLoja.Invalido;
            }
        }
    }
}