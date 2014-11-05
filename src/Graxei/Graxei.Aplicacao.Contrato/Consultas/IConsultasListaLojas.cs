using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasListaLojas
    {
        ListaLojas Get(int pagina, int tamanhoPagina);
    }

}
