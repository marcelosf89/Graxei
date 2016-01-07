using Graxei.Transversais.Idiomas;
using System.ComponentModel.DataAnnotations;

namespace Graxei.Transversais.ContratosDeDados
{
    public class PesquisaProdutoContrato
    {
        public long IdLoja { get; set; }

        public long IdUnicoEndereco { get; set; }

        [Required(ErrorMessageResourceType = typeof(Validacoes), ErrorMessageResourceName="PesquisaMeuProdutoDescricao")]
        public string DescricaoProduto { get; set; }
        
        public bool MeusProdutos { get; set; }

        public int PaginaAtualLista { get; set; }

        public long TotalElementosLista { get; set; }
    }
}