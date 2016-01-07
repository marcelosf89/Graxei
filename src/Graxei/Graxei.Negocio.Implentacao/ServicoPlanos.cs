using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoPlanos : ServicoPadraoEntidades<Plano>, IServicoPlanos
    {
        public ServicoPlanos(IRepositorioPlanos reposUsuarios)
        {
            RepositorioEntidades = reposUsuarios;
        }

        public System.Collections.Generic.IList<Plano> GetPlanosAtivos()
        {
            return ((IRepositorioPlanos)RepositorioEntidades).GetPlanosAtivos();
        }

        public override void PreSalvar(Plano t)
        {
            throw new System.NotImplementedException();
        }

        public override void PreAtualizar(Plano t)
        {
            throw new System.NotImplementedException();
        }
    }
}