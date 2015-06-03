using System.Collections.Generic;
using Graxei.Modelo;
using Graxei.Transversais.Comum.Entidades;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoEstados : IEntidadesIrrestrito<Estado>
    {
        Estado GetPorSigla(string sigla);
        Estado GetPorNome(string nome);
        IList<Estado> Todos(EstadoOrdem ordem);
    }
}
