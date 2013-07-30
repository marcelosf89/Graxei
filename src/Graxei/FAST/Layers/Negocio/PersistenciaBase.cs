using System.Collections.Generic;
using FAST.Layers.Data;
using FAST.Layers.Modelo;

namespace FAST.Layers.Negocio
{
    /// <summary>
    /// Classe base para as regras de negócio
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public abstract class PersistenciaBase<TEntity, TId> where TEntity : EntidadeBase<TId>
    {
        #region Fields
        private IData<TEntity, TId> _dataObject;
        #endregion

        #region Properties
        /// <summary>
        /// Grava e recupera o DataObject
        /// </summary>
        protected IData<TEntity, TId> DataObject
        {
            get { return this._dataObject; }
            set { this._dataObject = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor padrão que inicializa o data object pela classe filha
        /// </summary>
        protected PersistenciaBase()
        {
            this.SetDataObject();
        }
        #endregion

        #region Protected Abstract Methods
        /// <summary>
        /// Grava o data object, deve ser implementado pelas classes filhas
        /// </summary>
        protected abstract void SetDataObject();
        #endregion


        #region Public Virtual Methods
        /// <summary>
        /// Recupera todas as entidades
        /// </summary>
        /// <returns>Interface de lista com as entidades</returns>
        public virtual IList<TEntity> GetAll()
        {
            return this.DataObject.GetAll();
        }

        /// <summary>
        /// Recupera a entidade que possui o ID
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade que possui o ID</returns>
        public virtual TEntity GetById(TId id)
        {
            return this.DataObject.GetById(id);
        }

        /// <summary>
        /// Grava a entidade
        /// TESTE DE VALIDADE PARA SALVAR : Se a entidade não foi falidada então ira retornar true mas a entidade nao sera salva;
        /// </summary>
        /// <param name="entity">Entidade para persistencia</param>
        /// <returns>True ou False</returns>
        public virtual void Save(TEntity entity)
        {

            //Se a entidade não foi falidada então ira retornar true mas a entidade nao sera salva
            if (!entity.Id.Equals(-1))
            {
                this.DataObject.Save(entity);
            }
        }

        /// <summary>
        /// Atualiza uma entidade
        /// </summary>
        /// <param name="entity">Entidade para atualização</param>
        /// <returns>True ou False</returns>
        public virtual void Update(TEntity entity)
        {
            this.DataObject.Update(entity);
        }

        public virtual void Merge(TEntity entity)
        {
            this.DataObject.Merge(entity);
        }

        /// <summary>
        /// Remove uma entidade
        /// </summary>
        /// <param name="entity">Entidade para remoção</param>
        /// <returns>True ou False</returns>
        public virtual void Delete(TEntity entity)
        {
            this.DataObject.Delete(entity);
        }
        #endregion
    }
}
