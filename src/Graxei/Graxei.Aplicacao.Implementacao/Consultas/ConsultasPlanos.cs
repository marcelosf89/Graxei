using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Modelo;
using Graxei.Negocio.Contrato;

namespace Graxei.Aplicacao.Implementacao.Consultas
{
    public class ConsultasPlanos : IConsultasPlanos
    {
        #region Construtor
        public ConsultasPlanos(IServicoPlanos servicoPlanos)
        {
            ServicoPlanos = servicoPlanos;
        }
        #endregion

        public IServicoPlanos ServicoPlanos
        {
            get;
            private set;
        }

        public System.Collections.Generic.IList<Plano> GetPlanosAtivos()
        {
            return ServicoPlanos.GetPlanosAtivos();
        }
    }
}