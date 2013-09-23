using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoEstados : ServicoPadraoEntidades<Estado>, IServicoEstados
    {
        #region Construtor
        public ServicoEstados(IRepositorioEstados repositorio)
        {
            _repositorioEntidades = repositorio;
        }
        #endregion

        #region Implementação de IServicoEstados

        public Estado GetPorSigla(string sigla)
        {
            return Repositorio.GetPorSigla(sigla);
        }

        public Estado GetPorNome(string nome)
        {
            return Repositorio.GetPorNome(nome);
        }

        public IList<Estado> Todos(EstadoOrdem ordem)
        {
            return Repositorio.Todos(ordem);
        }

        #endregion

        #region Propriedades Privadas
        private IRepositorioEstados Repositorio { get { return (IRepositorioEstados) _repositorioEntidades;  } }
        #endregion

    }
}