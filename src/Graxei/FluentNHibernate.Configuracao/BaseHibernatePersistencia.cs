using System;
using System.Collections;
using System.Collections.Generic;
using FAST.Layers.Modelo;
using FAST.Layers.Negocio;
using NHibernate;
using NHibernate.Criterion;
using Graxei.FluentNHibernate.Configuracao;

namespace Graxei.FluentNHibernate
{
    /// <summary>
    /// Classe abstrata básica para regra de negócios
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do identificador da entidade</typeparam>
    public abstract class BaseHibernatePersistencia<TEntity, TId> : PersistenciaBase<TEntity, TId> where TEntity : EntidadeBase<TId>
    {
        #region Fields
        protected DataGenerico<TEntity, TId> _genericDao;
        protected string _user;
        #endregion

        #region Properties
        public string MessageErro { get; set; }
        #endregion

        #region Protected Override Methods
        /// <summary>
        /// Grava o objeto de dados
        /// </summary>
        protected override void SetDataObject()
        {
            this.DataObject = this._genericDao = new DataGenerico<TEntity, TId>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Captura a sessão aberta.
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return NHibernateSessionPerRequest.GetCurrentSession();
        }


        /// <summary>
        /// Limpa as criterias da busca personalizada
        /// </summary>
        public void ClearCriterion()
        {
            this._genericDao.ICriterions.Clear();
        }

        /// <summary>
        /// Adiciona uma critéria a busca personalizada
        /// </summary>
        /// <param name="iCriterion">Interface de criterion</param>
        public void AddCriterion(ICriterion iCriterion)
        {
            this._genericDao.ICriterions.Add(iCriterion);
        }

        /// <summary>
        /// Adiciona uma projeção a busca personalizada
        /// </summary>
        /// <param name="iProjection">Interface de projection</param>
        public void SetProjection(IProjection iProjection)
        {
            this._genericDao.IProjection = iProjection;
        }

        /// <summary>
        /// Adiciona uma órdem a uma busca
        /// </summary>
        /// <param name="order">Órdem de busca</param>
        public void AddOrder(Order order)
        {
            this._genericDao.Order = order;
        }

        /// <summary>
        /// Adiciona uma quantidade para retornar na lista
        /// </summary>
        /// <param name="order">Quantidade de registro retornado</param>
        public void AddMaxResult(int setMaxResult)
        {
            this._genericDao.SetMaxResult = setMaxResult;
        }

        public void AddMax(string Property)
        {
            this._genericDao.SetMax = Property;
        }
        

        /// <summary>
        /// Carrega instâncias do tipo passado filtradas pelas criterias
        /// ; OBS.: Busca por entidades dentro de entidades é feita usando '->'
        /// ; Ex.: Entidade->SubEntidade.Propriedade
        /// </summary>
        /// <param name="criterion">Criterias para filtro</param>
        /// <returns>Lista com instâncias filtradas ou vazia</returns>
        public IList<TEntity> GetByCriteria(params ICriterion[] criterion)
        {
            IList<TEntity> result = this._genericDao.GetByCriteria(criterion);
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
            TEntity result = this._genericDao.GetUniqueByCriteria(criterion);
            return result;
        }


        /// <summary>
        /// Carrega instâncias da entidade passada filtradas por criterias e projeções
        /// </summary>
        /// <typeparam name="T">Tipo da entidade</typeparam>
        /// <returns>IList da entidade Imovel</returns>
        public IList<T> GetByCustomCriteria<T>()
        {
            IList<T> result = this.GetByCustomCriteria<T>(false);
            return result;
        }

        /// <summary>
        /// Carrega todas as instancias de uma entidade
        /// </summary>
        /// <returns></returns>
        public override IList<TEntity> GetAll()
        {
            IList<TEntity> result = base.GetAll();
            return result;
        }

        /// <summary>
        /// Carrega a instancias de uma entidade
        /// </summary>
        /// <returns></returns>
        public override TEntity GetById(TId id)
        {
            TEntity result = base.GetById(id);
            return UnProxyObjectAs<TEntity>(result);
        }

        public TEntity UnProxyObjectAs<TEntity>(object obj)
        {
            return (TEntity)this.GetSession().GetSessionImplementation().PersistenceContext.Unproxy(obj);
        }
        /// <summary>
        /// Carrega instâncias da entidade passada filtradas por criterias e projeções
        /// </summary>
        /// <typeparam name="T">Tipo da entidade</typeparam>
        /// <param name="maintainCriteria">True para manter as criterias, false para apagar as criterias</param>
        /// <returns>IList da entidade Imovel</returns>
        public IList<T> GetByCustomCriteria<T>(bool maintainCriteria)
        {
            IList objects = this._genericDao.GetByCustomCriteria();
            T[] output = new T[objects.Count];
            objects.CopyTo(output, 0);
            if (!maintainCriteria)
            {
                this.ClearCriterion();
            }
            return output;
        }

        /// <summary>
        /// Verifica se existe o valor no banco.
        /// </summary>
        /// <param name="column">Coluna a ser verificada.</param>
        /// <param name="value">Valor</param>
        /// <returns></returns>
        public bool Contains(string column, object value)
        {
            bool retorno = false;

            if (value != null)
            {
                List<ICriterion> criterions = new List<ICriterion>();
                criterions.Add(Expression.Eq(column, value));
                retorno = (this.GetByCriteria(criterions.ToArray()).Count > 0);
            }
            return retorno;
        }

        /// <summary>
        /// Verifica se existe o valor no banco diferente do ID da entidade.
        /// </summary>
        /// <param name="column">Coluna a ser verificada.</param>
        /// <param name="value">Valor</param>
        /// <returns></returns>
        public bool Contains(string column, object value, object id)
        {
            bool retorno = false;
            if (value != null)
            {
                List<ICriterion> criterions = new List<ICriterion>();
                criterions.Add(Expression.Eq(column, value));
                if (id != null)
                    criterions.Add(Expression.Not(Expression.Eq("Id", id)));
                retorno = (this.GetByCriteria(criterions.ToArray()).Count > 0);
            }
            return retorno;
        }

        #endregion

        public global::NHibernate.ISQLQuery Query(string query)
        {
            return _genericDao.Query(query);
        }

        public IList QueryList(string query)
        {
            return _genericDao.Query(query).List();
        }

        public object QueryUnique(string query)
        {
            return _genericDao.Query(query).UniqueResult();
        }

        public IQuery GetNamedQueryHQL(String NamedQuery, ISession session)
        {
            return this.GetSession().CreateQuery(this.GetSession().GetNamedQuery(NamedQuery).QueryString);
        }

        public IQuery GetNamedQueryHQL(String NamedQuery)
        {
            return GetNamedQueryHQL(NamedQuery, null);
        }

        public override void Save(TEntity entity)
        {
            base.Save(entity);
        }
        public override void Delete(TEntity entity)
        {
            base.Delete(entity);
        }
        public override void Update(TEntity entity)
        {
            base.Update(entity);
        }

    }
}

