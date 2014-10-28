using Graxei.Modelo;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasLojas
    {
        Loja Get(long id);
        Loja GetPorNome(string nome);
        Loja GetComEnderecos(long id);
    }
}
