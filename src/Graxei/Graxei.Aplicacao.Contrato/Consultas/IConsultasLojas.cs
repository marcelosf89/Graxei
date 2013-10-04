using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasLojas
    {
        IServicoLojas ServicoLojas { get; }
        IServicoEnderecos ServicoEnderecos { get; }
        IServicoTelefones ServicoTelefones { get; }
    }
}
