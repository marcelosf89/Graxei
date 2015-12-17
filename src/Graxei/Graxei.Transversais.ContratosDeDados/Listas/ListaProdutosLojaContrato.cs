using Graxei.Transversais.ContratosDeDados.Interfaces;
namespace Graxei.Transversais.ContratosDeDados.Listas
{
    public sealed class ListaProdutosLojaContrato : IItemLista
    {
        public long Id { get; set; }

        public string Codigo { get; set; }

        public string MinhaDescricao { get; set; }

        public string DescricaoOriginal { get; set; }

        public long IdEndereco { get; set; }

        public long? IdMeuProduto { get; set; }

        public double Preco { get; set; }

        public string PrecoMascara
        {
            get
            {
                return Preco.ToString("N");
            }
        }

        public bool Excluido { get; set; }

        public string Descricao
        {
            get
            {
                return string.IsNullOrEmpty(MinhaDescricao) ? DescricaoOriginal : MinhaDescricao;
            }
        }
        public bool IsMeuProduto
        {
            get
            {
                return IdMeuProduto != null && !Excluido;
            }

        }

        public override bool Equals(object obj)
        {
            if (!(obj is ListaProdutosLojaContrato) || obj == null)
            {
                return false;
            }

            ListaProdutosLojaContrato that = (ListaProdutosLojaContrato)obj;
            return (this.Id == that.Id && this.Descricao == that.Descricao);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ 11 + this.Descricao.GetHashCode() ^ 9;
        }
    }
}
