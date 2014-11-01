namespace Graxei.Transversais.ContratosDeDados
{
    public class EnderecoListaContrato
    {
        public EnderecoListaContrato(long idEndereco, string descricaoEndereco)
        {
            IdEndereco = idEndereco;
            DescricaoEndereco = descricaoEndereco;
        }

        public long IdEndereco { get; private set; }
        public string DescricaoEndereco { get; private set; }
    }
}