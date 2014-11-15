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
        public string Descricao { get; set; }
        public long EnderecoId { get; set; }
        public long ProdutoId { get; set; }
        public string Numero { get; set; }
        public double Preco { get; set; }
    }
}