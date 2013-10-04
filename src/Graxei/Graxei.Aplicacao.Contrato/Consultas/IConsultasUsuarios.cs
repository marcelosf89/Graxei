using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasUsuarios
    {
        IServicoUsuarios ServicoUsuarios { get;  }
        Usuario GetPorLogin(string p);
    }
}