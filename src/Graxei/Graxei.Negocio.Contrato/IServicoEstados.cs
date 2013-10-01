using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Transversais.Utilidades.Entidades;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoEstados : IEntidadesIrrestrito<Estado>
    {
        Estado GetPorSigla(string sigla);
        Estado GetPorNome(string nome);
        IList<Estado> Todos(EstadoOrdem ordem);
    }
}
