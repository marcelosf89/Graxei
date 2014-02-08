using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoTelefones : ServicoPadraoSomenteLeitura<Telefone>, IServicoTelefones
    {
        public ServicoTelefones(IRepositorioTelefones repositorioTelefones)
        {
            RepositorioEntidades = repositorioTelefones;
        }

        #region Propriedades Privadas
        private IRepositorioTelefones Repositorio { get { return (IRepositorioTelefones)RepositorioEntidades; } }
        #endregion

        #region Implementação de IServicoTelefones

        public IList<Telefone> Todos(long idEndereco)
        {
            return Repositorio.Todos(idEndereco);
        }

        #endregion

        #region Implementation of IEntidadesExcluir<Telefone>

        public void PreExcluir(Telefone t)
        {
            throw new System.NotImplementedException();
        }

        public void Excluir(Telefone t)
        {
            /* TODO: implementar */
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
