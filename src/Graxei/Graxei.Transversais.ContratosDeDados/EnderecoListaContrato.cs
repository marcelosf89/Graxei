namespace Graxei.Transversais.ContratosDeDados
{
    public class EnderecoListaContrato
    {
        public EnderecoListaContrato(long idEndereco, string descricaoEndereco, string cnpj)
        {
            IdEndereco = idEndereco;
            DescricaoEndereco = descricaoEndereco;
            Cnpj = cnpj;
        }

        public long IdEndereco { get; private set; }
        public string DescricaoEndereco { get; private set; }
        public string Cnpj { get; private set; }
    }
}