using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoLogradouros : ServicoPadraoEntidades<Logradouro>, IServicoLogradouros
    {

        #region Construtor
        public ServicoLogradouros(IRepositorioLogradouros repositorio)
        {
           RepositorioEntidades = repositorio;
        }

        #endregion

        #region Implementação de IServicoBairros

        public Logradouro Get(string nome)
        {
            /* TODO */
            throw new System.NotImplementedException();
        }

        public IList<Logradouro> Get(Bairro bairro)
        {
            return Repositorio.Get(bairro);
        }

        public IList<Logradouro> GetPorCidade(long idBairro)
        {
            return Repositorio.Get(idBairro);
        }

        public IList<Logradouro> GetPorBairro(string nomeBairro, string nomeCidade, long idEstado)
        {
            return Repositorio.GetPorBairro(nomeBairro, nomeCidade, idEstado);
        }

        public Logradouro Get(string nomeLogradouro, string nomeBairro, long idCidade)
        {
            return Repositorio.Get(nomeLogradouro, nomeBairro, idCidade);
        }

        public Logradouro Get(string nomeLogradouro, string nomeBairro, Cidade cidade)
        {
            return Repositorio.Get(nomeLogradouro, nomeBairro, cidade);
        }

        public Logradouro Get(string nomeLogradouro, long idBairro)
        {
            return Repositorio.Get(nomeLogradouro, idBairro);
        }

        public Logradouro Get(string nomeLogradouro, Bairro bairro)
        {
            return Repositorio.Get(nomeLogradouro, bairro);
        }

        #endregion

        #region Propriedades Privadas

        private IRepositorioLogradouros Repositorio
        {
            get
            {
                return (IRepositorioLogradouros)RepositorioEntidades;
            }
        }

        #endregion

        public IList<Logradouro> GetPorBairro(long idBairro)
        {
            throw new System.NotImplementedException();
        }

        #region Overrides of ServicoPadraoEntidades<Logradouro>

        public override void PreSalvar(Logradouro t)
        {
            throw new System.NotImplementedException();
        }

        public override void PreAtualizar(Logradouro t)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}