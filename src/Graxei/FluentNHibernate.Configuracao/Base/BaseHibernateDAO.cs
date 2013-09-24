using System;
using System.Collections.Generic;
using FAST.Layers.Data;
using FAST.Layers.Modelo;
using FAST.Log;
using Graxei.FluentNHibernate.UnitOfWork;
using NHibernate.Criterion;
using Graxei.FluentNHibernate.Configuracao;

namespace Graxei.FluentNHibernate.Base
{
    /// <summary>
    /// Classe abstrata b�sica para acesso a dados
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do identificador da entidade</typeparam>
    public abstract class BaseHibernateData<TEntity, TId> : IData<TEntity, TId> where TEntity : EntidadeBase<TId>
    {
        #region IData<TEntity,TId> Members
        /// <summary>
        /// Apaga uma inst�ncia da entidade
        /// </summary>
        /// <param name="entity">Inst�ncia da entidade</param>
        /// <returns>True ou false</returns>
        public void Delete(TEntity entity)
        {
            try
            {
                UnitOfWorkNHibernate.GetCurrentSession().Delete(entity);
                UnitOfWorkNHibernate.GetCurrentSession().Flush();
                
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Delete Exception", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Recupera todos as inst�ncias da entidade
        /// </summary>
        /// <returns>Interface de lista com todos as inst�ncias da entidade</returns>
        public IList<TEntity> GetAll()
        {
            IList<TEntity> result = UnitOfWorkNHibernate.GetCurrentSession().CreateCriteria(typeof(TEntity)).List<TEntity>();
            
            return result;
        }

        /// <summary>
        /// Recupera uma inst�ncia da entidade
        /// </summary>
        /// <param name="id">Identificador da inst�ncia</param>
        /// <returns>Inst�ncia da entidade</returns>
        public TEntity GetById(TId id)
        {
            //List<ICriterion> criterion = new List<ICriterion>();
            //criterion.Add(Expression.Eq("Id", id));

            TEntity result = UnitOfWorkNHibernate.GetCurrentSession().CreateCriteria(typeof(TEntity))
                                     .Add(Expression.Eq("Id", id))
                                     .UniqueResult<TEntity>();
            /*foreach (ICriterion criterium in criterion)
            {
                //this.createAliases(criteria, criterium);
                criteria.Add(criterium);
            }
            IList<TEntity> foundList = criteria.List<TEntity>() as List<TEntity>;
            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }
            else if (foundList.Count == 1)
            {
                result = foundList[0];
            }
            else
            {
                result = default(TEntity);
            }*/

           return result;
        }

        /// <summary>
        /// Grava uma inst�ncia da entidade
        /// </summary>
        /// <param name="entity">Inst�ncia da entidade</param>
        /// <returns>True ou false</returns>
        public void Save(TEntity entity)
        {
            try
            {
                UnitOfWorkNHibernate.GetCurrentSession().Save(entity);
                UnitOfWorkNHibernate.GetCurrentSession().Flush();
                
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Save Exception", exception);
                throw exception;
            }

        }

        /// <summary>
        /// Grava uma inst�ncia da entidade
        /// </summary>
        /// <param name="entity">Inst�ncia da entidade</param>
        /// <param name="query">Query j� montada para fazer o Pos Save</param>
        /// <returns>True ou false</returns>
        public void Save(TEntity entity, string query)
        {
            try
            {
                UnitOfWorkNHibernate.GetCurrentSession().Save(entity);
                this.QuerySQLSaveOrUpdate(query);
                
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Save Exception", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Atualizar uma inst�ncia da entidade
        /// </summary>
        /// <param name="entity">Inst�ncia da entidade</param>
        /// <param name="query">Query j� montada para fazer o Pos Save</param>
        /// <returns>True ou false</returns>
        public void Update(TEntity entity, string query)
        {
            try
            {
                UnitOfWorkNHibernate.GetCurrentSession().Update(entity);
                this.QuerySQLSaveOrUpdate(query);
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Save Exception", exception);
                throw exception;
            }
        }


        /// <summary>
        /// Atualiza uma inst�ncia da entidade
        /// </summary>
        /// <param name="entity">Inst�ncia da entidade</param>
        /// <returns>True ou false</returns>
        public void Update(TEntity entity)
        {
            try
            {
                UnitOfWorkNHibernate.GetCurrentSession().Merge(entity);
                UnitOfWorkNHibernate.GetCurrentSession().Flush();
                
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Update Exception", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Atualiza uma inst�ncia da entidade
        /// </summary>
        /// <param name="entity">Inst�ncia da entidade</param>
        /// <returns>True ou false</returns>
        public void Merge(TEntity entity)
        {
            try
            {
                UnitOfWorkNHibernate.GetCurrentSession().Merge(entity);
                UnitOfWorkNHibernate.GetCurrentSession().Flush();
                
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Merge Exception", exception);
                throw exception;
            }
        }

        /// <summary>
        /// Salva ou Atualiza a query passada
        /// </summary>
        /// <param name="query">Passa a QUERY j� montada."</param>
        /// <returns></returns>
        public void QuerySQLSaveOrUpdate(string query)
        {
            try
            {
                UnitOfWorkNHibernate.GetCurrentSession().CreateSQLQuery(query);
                UnitOfWorkNHibernate.GetCurrentSession().Flush();
                
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Query SQL Save or Update Exception", exception);
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">Passa a QUERY j� montada."</param>
        /// <returns></returns>
        public global::NHibernate.ISQLQuery Query(string query)
        {
            
            try
            {
                global::NHibernate.ISQLQuery retorno = UnitOfWorkNHibernate.GetCurrentSession().CreateSQLQuery(query);
                return retorno;
            }
            catch (Exception exception)
            {
                ExceptionLogger.Instance.LogException(Level.Error, "Query Exception", exception);
                return null;
            }
        }
        #endregion
    }
}
