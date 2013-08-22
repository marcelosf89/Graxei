using System.Collections.Generic;
using FAST.Layers.Model;
using FAST.Layers.Modelo;

namespace FAST.Layers.Data
{
    /// <summary>
    /// Interface para classes de acesso a dados
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public interface IData<TEntity, TId> where TEntity : EntidadeBase<TId>
    {
        /// <summary>
        /// Recupera todas as entidades
        /// </summary>
        /// <returns>Interface de lista com as entidades</returns>
        IList<TEntity> GetAll();

        /// <summary>
        /// Recupera a entidade que possui o ID
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Entidade que possui o ID</returns>
        TEntity GetById(TId id);

        /// <summary>
        /// Grava a entidade
        /// </summary>
        /// <param name="entity">Entidade para persistencia</param>
        /// <returns>True ou False</returns>
        void Save(TEntity entity);

        /// <summary>
        /// Atualiza uma entidade
        /// </summary>
        /// <param name="entity">Entidade para persistencia</param>
        /// <returns>True ou False</returns>
        void Update(TEntity entity);

        /// <summary>
        /// Atualiza uma entidade
        /// </summary>
        /// <param name="entity">Entidade para persistencia</param>
        /// <returns>True ou False</returns>
        void Merge(TEntity entity);

        /// <summary>
        /// Remove uma entidade
        /// </summary>
        /// <param name="entity">Entidade para persistencia</param>
        /// <returns>True ou False</returns>
        void Delete(TEntity entity);
    }
}
