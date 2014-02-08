using System;
using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Utilidades.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoCidades : ServicoPadraoEntidades<Cidade>, IServicoCidades
    {

        #region Construtor
        public ServicoCidades(IRepositorioCidades repositorio)
        {
           RepositorioEntidades = repositorio;
        }

        #endregion

        #region Métodos Privados
        private void ValidarEntidade(Cidade cidade)
        {
            if (cidade == null)
            {
                throw new ArgumentException(ErrosInternos.ArgumentoBairroNulo);
            }

            if (string.IsNullOrEmpty(cidade.Nome))
            {
                throw new EntidadeInvalidaException(Erros.CidadeNomeNulo);
            }
            if (!cidade.Validar())
            {
                throw new EntidadeInvalidaException(ErrosInternos.CidadeInvalida);
            }
        }
        #endregion

        #region Métodos Sobrescritos
        public override void PreSalvar(Cidade cidade)
        {
            ValidarEntidade(cidade);
            Cidade c = Repositorio.Get(cidade.Nome, cidade.Estado.Id);
            if (c != null)
            {
                throw new ObjetoJaExisteException(Erros.CidadeJaExiste);
            }
        }

        public override void PreAtualizar(Cidade cidade)
        {
           ValidarEntidade(cidade);
           Cidade c = Repositorio.GetPorId(cidade.Id);
           if (!c.Equals(cidade))
           {
               throw new OperacaoEntidadeException(ErrosInternos.ProibidoAlterarCidade);
           }
           c = Repositorio.Get(cidade.Nome, cidade.Estado.Id);
           if (c != null && c.Id != cidade.Id)
           {
               throw new ObjetoJaExisteException(Erros.CidadeJaExiste);
           }
        }

        #endregion

        #region Implementação de IServicoCidades

        public Cidade Get(string nome, long idEstado)
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

        public IList<Cidade> GetPorEstado(long idEstado)
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
                return (IRepositorioCidades)RepositorioEntidades;
            }
        }

        #endregion
    }
}