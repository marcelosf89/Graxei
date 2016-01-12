namespace Graxei.Transversais.ContratosDeDados
{
    public class ProdutoLojaPrecoContrato
    {
        public long Id { get; set; }

        public long IdMeuProduto { get; set; }

        public string MinhaDescricao { get; set; }

        public long IdEndereco { get; set; }

        public double Preco { get; set; }

        public OperacaoProdutoLoja OperacaoNoContrato
        {
            get
            {
                if (IdMeuProduto > 0)
                {
                    if (Preco <= 0)
                    {
                        return OperacaoProdutoLoja.Excluir;
                    }

                    return OperacaoProdutoLoja.Alterar;
                }
                else if (Id > 0 && IdEndereco > 0 && Preco > 0)
                {
                    return OperacaoProdutoLoja.Incluir;
                }

                return OperacaoProdutoLoja.Invalido;
            }
        }
    }
}