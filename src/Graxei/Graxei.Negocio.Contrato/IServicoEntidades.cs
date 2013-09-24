using FAST.Modelo;
using Graxei.Persistencia.Contrato;
using System.Collections.Generic;

namespace Graxei.Negocio.Contrato
{
    /// <summary>
    /// Interface para gerenciamentos CRUD de entidades
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IServicoEntidades<T> where T : Entidade
    {
        void Salvar(T t);
        void Excluir(T t);
        T GetPorId(long id);
        IList<T> Todos();
        IRepositorioEntidades<T> RepositorioEntidades { get; }
    }
}