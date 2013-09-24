using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoBairros : ServicoPadraoEntidades<Bairro>, IServicoBairros
    {

        #region Construtor
        public ServicoBairros(IRepositorioBairros repositorio)
        {
            _repositorioEntidades = repositorio;
        }

        #endregion

        #region Implementação de IServicoBairros

        public Bairro Get(string nome)
        {
            throw new System.NotImplementedException();
        }

        public IList<Bairro> Get(Cidade cidade)
        {
            return Repositorio.Get(cidade);
        }

        public IList<Bairro> GetPorCidade(int idCidade)
        {
            return Repositorio.Get(idCidade);
        }

        public Bairro Get(string nomeBairro, string nomeCidade, int idEstado)
        {
            return Repositorio.Get(nomeBairro, nomeCidade, idEstado);
        }

        public Bairro Get(string nomeBairro, string nomeCidade, Estado estado)
        {
            return Repositorio.Get(nomeBairro, nomeCidade, estado);
        }

        public Bairro Get(string nomeBairro, int idCidade)
        {
            return Repositorio.Get(nomeBairro, idCidade);
        }

        public Bairro Get(string nomeBairro, Cidade cidade)
        {
            return Repositorio.Get(nomeBairro, cidade);
        }

        #endregion

        #region Propriedades Privadas

        private IRepositorioBairros Repositorio
        {
            get
            {
                return (IRepositorioBairros)_repositorioEntidades;
            }
        }

        #endregion
    }
}