using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasLojas
    {
        Loja Get(long id);
        Loja GetPorNome(string nome);
        LojaContrato GetComEnderecos(long id);
    }
}
