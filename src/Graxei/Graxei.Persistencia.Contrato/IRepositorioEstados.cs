using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioEstados : IRepositorioEntidades<Estado>
    {
        Estado GetPorSigla(string sigla);
        Estado GetPorNome(string nome);
        IList<Estado> Todos(EstadoOrdem ordem);
    }
}
