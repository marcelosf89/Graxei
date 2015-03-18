using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultaLogin : IConsultaLogin
    {
        public ConsultaLogin(IServicoUsuarios servicoUsuarios)
        {
            ServicoUsuarios = servicoUsuarios;
        }

        #region Implementação de IConsultasLogin

        public Usuario AutenticarPorLogin(string login, string senha)
        {
            return ServicoUsuarios.AutenticarPorLogin(login, senha);
        }

        public Usuario GetPorNome(string nome)
        {
            return ServicoUsuarios.GetPorNome(nome);
        }
        
        public IServicoUsuarios ServicoUsuarios { get; private set; }

        #endregion

    }
}