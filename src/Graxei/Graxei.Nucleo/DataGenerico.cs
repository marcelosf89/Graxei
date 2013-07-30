using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FAST.Layers.Modelo;
using Graxei.Nucleo.Base;
using Graxei.Nucleo.NHibernate.GerenciarSessao;
using NHibernate;
using NHibernate.Criterion;

namespace Graxei.Nucleo
{
    /// <summary>
    /// Classe genérica para acesso a dados
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do identificador da entidade</typeparam>
    public sealed class DataGenerico<TEntity, TId> : BaseHibernateData<TEntity, TId> where TEntity : EntidadeBase<TId>
    {
        #region Fields
        private IList<ICriterion> _iCriterions;
        private IProjection _iProjection;
        private List<string> _aliases;
        private Order _order;
        private int _setMaxResult;
        private string _setMax;
        #endregion

        #region Properties
        public bool IsSessionController { get; set; }

        /// <summary>
        /// Grava e recupera a lista de critérias
        /// </summary>
        public IList<ICriterion> ICriterions
        {
            get { return this._iCriterions ?? (this._iCriterions = new List<ICriterion>()); }
        }

        /// <summary>
        /// Grava e recupera a projeção
        /// </summary>
        public IProjection IProjection
        {
            get { return this._iProjection; }
            set { this._iProjection = value; }
        }

        /// <summary>
        /// Grava e recupera a ordem de busca
        /// </summary>
        public Order Order
        {
            get { return this._order; }
            set { this._order = value; }
        }

        public int SetMaxResult
        {
            get { return this._setMaxResult; }
            set { this._setMaxResult = value; }
        }

        public string SetMax
        {
            get { return this._setMax; }
            set { this._setMax = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Construtor padrão, executa método de inicialização de campos da classe
        /// </summary>
        public DataGenerico()
        {
            this.initFields();
        }
        /// <summary>
        /// Construtor padrão, executa método de inicialização de campos da classe
        /// </summary>

        #endregion

        #region Private Methods
        /// <summary>
        /// Inicializa os campos da classe
        /// </summary>
        private void initFields()
        {
            this._aliases = new List<string>();
        }

        /// <summary>
        /// Cria os aliases para entidades internas serem usadas em criterias
        /// </summary>
        /// <param name="criteria">A criteria criada para a busca</param>
        /// <param name="criterion">A expressão avaliada</param>
        private void createAliases(ICriteria criteria, ICriterion criterion)
        {
            string expression = criterion.ToString();
            expression = Regex.Replace(expression, "( .*)", "");
            string[] entities = expression.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            if (entities.Length > 1)
            {
                string association = entities[0];
                if (!this._aliases.Contains(association))
                {
                    criteria.CreateAlias(association, association);
                    this._aliases.Add(association);
                }
                for (int it = 1; it < entities.Length - 1; it++)
                {
                    association += "." + entities[it];
                    if (!this._aliases.Contains(association))
                    {
                        criteria.CreateAlias(association, association.Replace(".", "_H_"));
                        this._aliases.Add(association);
                    }
                }
            }
        }

        /// <summary>
        /// Cria os aliases para entidades internas serem usadas na projeção
        /// </summary>
        /// <param name="criteria">A criteria criada para a busca</param>
        private void createProjectionAlias(ICriteria criteria)
        {
            string expression = this.IProjection.ToString();
            Regex regex = new Regex("( .*)");
            expression = regex.Replace(expression, "");
            string[] entities = expression.Split(new string[] { "->", "." }, StringSplitOptions.RemoveEmptyEntries);
            if (entities.Length > 1)
            {
                string association = entities[0];
                if (!this._aliases.Contains(association))
                {
                    criteria.CreateAlias(association, association);
                }
                for (int it = 1; it < entities.Length - 1; it++)
                {
                    association += "." + entities[it];
                    if (!this._aliases.Contains(association))
                    {
                        criteria.CreateAlias(association, association.Replace(".", "->"));
                    }
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Carrega instâncias do tipo passado filtradas pelas criterias
        /// </summary>
        /// <param name="criterion">Criterias para filtro</param>
        /// <returns>Lista com instâncias filtradas ou vazia</returns>
        public IList<TEntity> GetByCriteria(params ICriterion[] criterion)
        {
            ICriteria criteria = NHibernateSessionPerRequest.GetCurrentSession().CreateCriteria(typeof(TEntity));
            if (this._order != null)
            {
                criteria.AddOrder(this._order);
            }
            if (this._setMaxResult > 0)
            {
                criteria.SetMaxResults(this._setMaxResult);
            }
            foreach (ICriterion criterium in criterion)
            {
                this.createAliases(criteria, criterium);
                criteria.Add(criterium);
            }
            this._aliases.Clear();
            this._order = null;
            List<TEntity> result = criteria.List<TEntity>() as List<TEntity>;
            return result;
        }

        /// <summary>
        /// Carrega uma instância do tipo passado filtrado pelas criterias
        /// </summary>
        /// <param name="criterion">Criterias para filtro</param>
        /// <returns>Instâncias do tipo passado</returns>
        /// <exception cref="NonUniqueResultException">Resultado da busca não é único</exception>
        public TEntity GetUniqueByCriteria(params ICriterion[] criterion)
        {
            IList<TEntity> foundList = this.GetByCriteria(criterion);
            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }
            else if (foundList.Count == 1)
            {
                return foundList[0];
            }
            else
            {
                return default(TEntity);
            }
        }

        /// <summary>
        /// Carrega instâncias da entidade passada filtradas por criterias e projeções
        /// </summary>
        /// <returns>IList da entidade passada</returns>
        public IList GetByCustomCriteria()
        {
            ICriteria criteria = NHibernateSessionPerRequest.GetCurrentSession().CreateCriteria(typeof(TEntity));
            if (this._order != null)
            {
                criteria.AddOrder(this._order);
            }
            if (this._setMaxResult > 0)
            {
                criteria.SetMaxResults(this._setMaxResult);
            }
            if (!string.IsNullOrEmpty(this._setMax))
            {
                criteria.SetProjection(Projections.Max(_setMax));
            }
            foreach (ICriterion criterium in this.ICriterions)
            {
                this.createAliases(criteria, criterium);
                criteria.Add(criterium);
            }
            this.createProjectionAlias(criteria);
            criteria.SetProjection(this.IProjection);
            this._aliases.Clear();
            this._order = null;
            IList result =  criteria.List();
            return result;
        }
        #endregion
    }
}
