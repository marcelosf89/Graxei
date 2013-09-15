using FAST.Modelo;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    /// <summary>
    /// Interface para gerenciamentos CRUD de entidades
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IServicoEntidade<T> where T : Entidade
    {
        void Salvar(T t);
        void Excluir(T t);
        T GetPorId(long id);
        IList<T> Todos();
    }
}