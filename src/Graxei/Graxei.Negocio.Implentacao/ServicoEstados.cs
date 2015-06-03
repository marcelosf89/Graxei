using System;
using System.Collections.Generic;
using System.Linq;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;
using Graxei.Persistencia.Contrato;
using Graxei.Transversais.Idiomas;
using Graxei.Transversais.Comum.Entidades;
using Graxei.Transversais.Comum.Excecoes;

namespace Graxei.Negocio.Implementacao
{
    public class ServicoEstados : ServicoPadraoEntidades<Estado>, IServicoEstados
    {
        #region Construtor
        public ServicoEstados(IRepositorioEstados repositorio)
        {
            RepositorioEntidades = repositorio;
        }
        #endregion

        #region Métodos Privados
        private void ValidarEntidade(Estado estado)
        {
            if (estado == null)
            {
                throw new ArgumentException(ErrosInternos.ArgumentoEstadoNulo);
            }
            if (string.IsNullOrEmpty(estado.Nome))
            {
                throw new EntidadeInvalidaException(Erros.EstadoNomeNulo);
            }
            if (string.IsNullOrEmpty(estado.Sigla))
            {
                throw new EntidadeInvalidaException(Erros.EstadoSiglaNula);
            }
            if (!estado.Validar())
            {
                throw new EntidadeInvalidaException(ErrosInternos.EstadoInvalido);
            }
        }
        #endregion

        #region Métodos Sobrescritos
        public override void PreSalvar(Estado estado)
        {
            ValidarEntidade(estado);
            IList<Estado> e = Repositorio.GetPorSiglaOuNome(estado.Sigla, estado.Nome);
            if (e.Any())
            {
                throw new ObjetoJaExisteException(Erros.EstadoJaExiste);
            }
        }

        public override void PreAtualizar(Estado estado)
        {
            ValidarEntidade(estado);
            IList<Estado> estados = Repositorio.GetPorSiglaOuNome(estado.Sigla, estado.Nome);
            int contador =
                estados.Count(p => p.Id != estado.Id);
            if (contador > 0)
            {
                throw new ObjetoJaExisteException(Erros.EstadoJaExiste);
            }
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
        private IRepositorioEstados Repositorio { get { return (IRepositorioEstados) RepositorioEntidades;  } }
        #endregion

    }
}