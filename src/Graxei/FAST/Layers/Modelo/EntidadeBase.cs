
namespace FAST.Layers.Modelo
{
    /// <summary>
    /// Classe base para as entidades
    /// </summary>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public abstract class EntidadeBase<TId>
    {
        #region Fields
        private TId _id;
        #endregion

        #region Properties
        /// <summary>
        /// Grava para entidades e recupera o ID
        /// </summary>
        public virtual TId Id
        {
            get { return this._id; }
            protected set { this._id = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor padrão protected
        /// ; Inicia o ID com default
        /// </summary>
        protected EntidadeBase()
        {
            this.Id = default(TId);
        }

        public EntidadeBase(TId id)
        {
            this.Id = id;
        }

        #endregion

        #region Override Methods
        /// <summary>
        /// Compara duas entidades, mesmo que uma delas seja transient
        /// </summary>
        /// <param name="obj">Entidade para teste</param>
        /// <returns>True ou False</returns>
        public override bool Equals(object obj)
        {
            EntidadeBase<TId> compareTo = obj as EntidadeBase<TId>;
            return (compareTo != null) &&
                   (this.haveSameContentAs(compareTo) &&
                   (this.haveSameNonDefaultIDAs(compareTo) ||
                   (this.IsTransient() || compareTo.IsTransient())));
        }

        /// <summary>
        /// Abstract, deve ser sobrescrito para retornar um hash para comparação
        /// </summary>
        public abstract override int GetHashCode();
        #endregion

        #region Private Methods
        /// <summary>
        /// Testa se a entidade possui o mesmo hash code de outra
        /// </summary>
        /// <param name="compareTo">Entidade para teste</param>
        /// <returns>True ou False</returns>
        private bool haveSameContentAs(EntidadeBase<TId> compareTo)
        {
            return this.GetHashCode() == compareTo.GetHashCode();
        }

        /// <summary>
        /// Testa se a entidade possui o mesmo ID não default de outra
        /// </summary>
        /// <param name="compareTo">Entidade para teste</param>
        /// <returns>True ou False</returns>
        private bool haveSameNonDefaultIDAs(EntidadeBase<TId> compareTo)
        {
            return (this.Id != null && !this.Id.Equals(default(TId))) &&
                   (compareTo.Id != null && !compareTo.Id.Equals(default(TId))) &&
                   this.Id.Equals(compareTo.Id);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Testa se a entidade é transient, isto é, não está associada a um item no banco de dados
        /// </summary>
        /// <returns>True ou False</returns>
        public virtual bool IsTransient()
        {
            return this.Id == null || this.Id.Equals(default(TId));
        }
        #endregion
    }
}
