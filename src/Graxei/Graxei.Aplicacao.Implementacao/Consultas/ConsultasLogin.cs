using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasLogin : IConsultasLogin
    {

        #region Construtor
        public ConsultasLogin(IServicoUsuarios servicoUsuarios)
        {
            ServicoUsuarios = servicoUsuarios;
        }
        #endregion

        #region Implementação de IConsultasLogin

        public Usuario AutenticarPorLogin(string login, string senha)
        {
            return ServicoUsuarios.AutenticarPorLogin(login, senha);
        }

        public Usuario AutenticarPorEmail(string email, string senha)
        {
            return ServicoUsuarios.AutenticarPorEmail(email, senha);
        }

        public IServicoUsuarios ServicoUsuarios { get; private set; }

        #endregion
    }
}