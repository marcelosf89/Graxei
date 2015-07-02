using Graxei.Transversais.ContratosDeDados.Interfaces;

namespace Graxei.Transversais.ContratosDeDados
{
    public class PesquisaContrato : IItemLista
    {
        public PesquisaContrato()
        {
        }

        public long Id { get; set; }

        public string Codigo { get; set; }

        public string DescricaoPadrao { get; set; }

        public string MinhaDescricao { get; set; }

        public long EnderecoId { get; set; }

        public long ProdutoId { get; set; }

        public string Numero { get; set; }

        public double Preco { get; set; }

        public string DescricaoExibir
        {
            get
            {
                if (string.IsNullOrEmpty(MinhaDescricao))
                {
                    return DescricaoPadrao;
                }

                return MinhaDescricao;
            }
        }

    }
}