
namespace FAST.Layers.Model
{
    /// <summary>
    /// Interface para permitir entidades a alterar o ID
    /// </summary>
    /// <typeparam name="TId">Tipo do ID da entidade</typeparam>
    public interface ICanSetId<TId>
    {
        /// <summary>
        /// Grava o ID
        /// </summary>
        /// <param name="id"></param>
        void SetIdTo(TId id);
    }
}
