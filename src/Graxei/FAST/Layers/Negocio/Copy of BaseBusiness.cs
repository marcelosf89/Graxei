using System.Collections.Generic;
using EDMFrame.Layers.Data;
using EDMFrame.Layers.Model;

namespace EDMFrame.Layers.Business
{
    /// <summary>
    /// Classe base para as regras de neg�cio
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public abstract class BaseBusiness<TEntity, TId> where TEntity : BaseEntity<TId>
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
        /// Construtor padr�o que inicializa o data object pela classe filha
        /// </summary>
        protected BaseBusiness()
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

        #region Protected Virtual Methods
        /// <summary>
        /// Procedimento a ser realizado antes de uma remo��o de registro
        /// </summary>
        /// <param name="entity">Entidade para remo��o</param>
        protected virtual void PreDelete(TEntity entity) { }

        /// <summary>
        /// Procedimento a ser realizado ap�s uma remo��o de registro
        /// </summary>
        /// <param name="entity">Entidade para remo��o</param>
        protected virtual void PosDelete(TEntity entity) { }

        /// <summary>
        /// Procedimento a ser realizado antes de uma persist�ncia do registro
        /// </summary>
        /// <param name="entity">Entidade para persist�ncia</param>
        protected virtual void PreSave(TEntity entity) { }

        /// <summary>
        /// Procedimento a ser realizado ap�s uma persist�ncia do registro
        /// </summary>
        /// <param name="entity">Entidade para persist�ncia</param>
        protected virtual void PosSave(TEntity entity) { }

        /// <summary>
        /// Procedimento a ser realizado antes de uma atualiza��o de registro
        /// </summary>
        /// <param name="entity">Entidade para persist�ncia</param>
        protected virtual void PreUpdate(TEntity entity) { }

        /// <summary>
        /// Procedimento a ser realizado ap�s uma atualiza��o de registro
        /// </summary>
        /// <param name="entity">Entidade para persist�ncia</param>
        protected virtual void PosUpdate(TEntity entity) { }

        /// <summary>
        /// Procedimento a ser realizado antes de uma persist�ncia/atualiza��o do registro
        /// </summary>
        /// <param name="entity">Entidade para persist�ncia</param>
        protected virtual void PreSaveUpdate(TEntity entity) { }

        /// <summary>
        /// Procedimento a ser realizado ap�s uma persist�ncia/atualiza��o do registro
        /// </summary>
        /// <param name="entity">Entidade para persist�ncia</param>
        protected virtual void PosSaveUpdate(TEntity entity) { }
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
        /// TESTE DE VALIDADE PARA SALVAR : Se a entidade n�o foi falidada ent�o ira retornar true mas a entidade nao sera salva;
        /// </summary>
        /// <param name="entity">Entidade para persistencia</param>
        /// <returns>True ou False</returns>
        public virtual void Save(TEntity entity)
        {
            this.PreSaveUpdate(entity);
            this.PreSave(entity);
            //Se a entidade n�o foi falidada ent�o ira retornar true mas a entidade nao sera salva
            if (!entity.Id.Equals(-1))
            {
                this.DataObject.Save(entity);
                
                this.PosSaveUpdate(entity);
                this.PosSave(entity);
                
            }
        }

        /// <summary>
        /// Atualiza uma entidade
        /// </summary>
        /// <param name="entity">Entidade para atualiza��o</param>
        /// <returns>True ou False</returns>
        public virtual void Update(TEntity entity)
        {
            this.PreSaveUpdate(entity);
            this.PreUpdate(entity);
            this.DataObject.Update(entity);
            this.PosSaveUpdate(entity);
            this.PosUpdate(entity);
        }

        public virtual void Merge(TEntity entity)
        {
            this.PreSaveUpdate(entity);
            this.PreUpdate(entity);
            this.DataObject.Merge(entity);
            this.PosSaveUpdate(entity);
            this.PosUpdate(entity);
        }

        /// <summary>
        /// Remove uma entidade
        /// </summary>
        /// <param name="entity">Entidade para remo��o</param>
        /// <returns>True ou False</returns>
        public virtual void Delete(TEntity entity)
        {
            this.PreDelete(entity);
            this.DataObject.Delete(entity);
            this.PosDelete(entity);
        }
        #endregion
    }
}
