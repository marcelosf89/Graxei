using Graxei.Aplicacao.Contrato.Consultas;
using Graxei.Negocio.Contrato;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Aplicacao.Implementacao
{
    public class ConsultasListaLojas : IConsultasListaLojas
    {
        private readonly IServicoListaLojas _servicoListaLojas;

        public ConsultasListaLojas(IServicoListaLojas servicoListaLojas)
        {
            _servicoListaLojas = servicoListaLojas;
        }

        public ListaLojas Get(int pagina, int tamanhoPagina)
        {
            return _servicoListaLojas.Get(pagina, tamanhoPagina);
        }
    }
}