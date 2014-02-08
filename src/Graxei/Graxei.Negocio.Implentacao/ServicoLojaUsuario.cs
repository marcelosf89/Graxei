using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoLojaUsuario :  ServicoPadraoSomenteLeitura<LojaUsuario>, IServicoLojaUsuario
    {

        #region Construtor
        public ServicoLojaUsuario(IRepositorioLojaUsuario repositorio)
        {
           RepositorioEntidades = repositorio;
        }
        #endregion


        #region Implementação of IServicoEntidades<LojaUsuario>

        public bool Existe(Loja loja, Usuario usuario)
        {
            return RepositorioLojaUsuario.Existe(loja, usuario);
        }

        #region Atributos Privados
        private IRepositorioLojaUsuario RepositorioLojaUsuario { get { return (IRepositorioLojaUsuario)RepositorioEntidades; } }
        #endregion


        #endregion
    }
}