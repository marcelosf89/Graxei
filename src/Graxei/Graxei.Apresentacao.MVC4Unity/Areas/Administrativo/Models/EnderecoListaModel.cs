namespace Graxei.Apresentacao.MVC4Unity.Areas.Administrativo.Models
{
    public class EnderecoListaModel
    {
        public EnderecoListaModel(long idEndereco, string descricaoEndereco)
        {
            IdEndereco = idEndereco;
            DescricaoEndereco = descricaoEndereco;
        }

        public long IdEndereco { get; private set; }
        public string DescricaoEndereco { get; private set; }
    }
}