using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasUsuarios : IConsultasUsuarios
    {
        #region Construtor
        public ConsultasUsuarios(IServicoUsuarios servicoUsuarios)
        {
            ServicoUsuarios = servicoUsuarios;
        }
        #endregion

        #region Implementação de IConsultasUsuarios
        public IServicoUsuarios ServicoUsuarios { get; private set; }
        public Usuario GetPorLogin(string login)
        {
            return ServicoUsuarios.GetPorLogin(login);
        }
        #endregion

    }
}