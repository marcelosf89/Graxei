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
            _repositorioEntidades = repositorioTelefones;
        }

        #region Propriedades Privadas
        private IRepositorioTelefones Repositorio { get { return (IRepositorioTelefones)_repositorioEntidades; } }
        #endregion

        #region Implementação de IServicoTelefones

        public IList<Telefone> Todos(long idEndereco)
        {
            return Repositorio.Todos(idEndereco);
        }

        #endregion

        #region Implementation of IEntidadesExcluir<Telefone>

        public void Excluir(Telefone t)
        {
            /* TODO: implementar */
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
