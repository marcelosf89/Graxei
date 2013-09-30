using System.Collections.Generic;
using Graxei.Modelo;
namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioLojas : IRepositorioEntidades<Loja>
    {
        /// <summary>
        /// Salva a loja e a associa com o usuário
        /// </summary>
        /// <param name="loja">A loja a ser salva</param>
        /// <param name="usuario">O usuário a ser associado à loja</param>
        /// <remarks>Caso seja a inserção da loja, sua associação é feita automaticamente. Se for atualização,
        ///          checa se esta associação já existe</remarks>
        void Salvar(Loja loja, Usuario usuario);
        /// <summary>
        /// Salva a loja e a associa com os usuários
        /// </summary>
        /// <param name="loja">A loja a ser salva</param>
        /// <param name="usuarios">Os usuários a serem associados à loja</param>
        /// <remarks>Caso seja a inserção da loja, sua associação com os usuários é feita automaticamente. Se for atualização,
        ///          para cada usuário na lista verifica-se se a associação já existe</remarks>
        void Salvar(Loja loja, IList<Usuario> usuarios);
        Loja Get(string nome);
    }
}
