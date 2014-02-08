using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoBairros : ServicoPadraoEntidades<Bairro>, IServicoBairros
    {

        #region Construtor
        public ServicoBairros(IRepositorioBairros repositorio)
        {
           RepositorioEntidades = repositorio;
        }

        #endregion
        #region Métodos Privados
        private void ValidarEntidade(Bairro bairro)
        {
            if (bairro == null)
            {
                throw new ArgumentException(ErrosInternos.ArgumentoBairroNulo);
            }

            if (string.IsNullOrEmpty(bairro.Nome))
            {
                throw new EntidadeInvalidaException(Erros.BairroNomeNulo);
            }
            if (!bairro.Validar())
            {
                throw new EntidadeInvalidaException(ErrosInternos.BairroInvalido);
            }
        }
        #endregion
        
        #region Métodos Sobrescritos
        public override void PreSalvar(Bairro bairro)
        {
            ValidarEntidade(bairro);
            Bairro b = Repositorio.Get(bairro.Nome, bairro.Cidade.Nome, bairro.Cidade.Estado.Id);
            if (b != null)
            {
                throw new ObjetoJaExisteException(Erros.BairroJaExiste);
            }
        }

        public override void PreAtualizar(Bairro bairro)
        {
           ValidarEntidade(bairro);
           Bairro b = Repositorio.Get(bairro.Nome, bairro.Cidade.Nome, bairro.Cidade.Estado.Id);
           if (b != null && b.Id != bairro.Id)
           {
               throw new ObjetoJaExisteException(Erros.BairroJaExiste);
           }
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

        public IList<Bairro> GetPorCidade(long idCidade)
        {
            return Repositorio.Get(idCidade);
        }

        public IList<Bairro> GetPorCidade(string nomeCidade, long idEstado)
        {
            return Repositorio.GetPorCidade(nomeCidade, idEstado);
        }

        public Bairro Get(string nomeBairro, string nomeCidade, long idEstado)
        {
            return Repositorio.Get(nomeBairro, nomeCidade, idEstado);
        }

        public Bairro Get(string nomeBairro, string nomeCidade, Estado estado)
        {
            return Repositorio.Get(nomeBairro, nomeCidade, estado);
        }

        public Bairro Get(string nomeBairro, long idCidade)
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
                return (IRepositorioBairros)RepositorioEntidades;
            }
        }

        #endregion

    }
}