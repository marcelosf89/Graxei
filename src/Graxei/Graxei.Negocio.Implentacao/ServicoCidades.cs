using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoCidades : ServicoPadraoEntidades<Cidade>, IServicoCidades
    {

        #region Construtor
        public ServicoCidades(IRepositorioCidades repositorio)
        {
            _repositorioEntidades = repositorio;
        }

        #endregion

        #region Implementação de IServicoCidades

        public Cidade Get(string nome, int idEstado)
        {
            return Repositorio.Get(nome, idEstado);
        }

        public Cidade Get(string nome, Estado estado)
        {
            return Repositorio.Get(nome, estado);
        }

        public IList<Cidade> Get(Estado estado)
        {
            return Repositorio.Get(estado);
        }

        public IList<Cidade> GetPorEstado(int idEstado)
        {
            return Repositorio.GetPorEstado(idEstado);
        }

        public IList<Cidade> GetPorSiglaEstado(string sigla)
        {
            return Repositorio.GetPorSiglaEstado(sigla);
        }

        public IList<Cidade> GetPorNomeEstado(string nome)
        {
            return Repositorio.GetPorNomeEstado(nome);
        }

        #endregion

        #region Propriedades Privadas

        private IRepositorioCidades Repositorio
        {
            get
            {
                return (IRepositorioCidades)_repositorioEntidades;
            }
        }

        #endregion
    }
}