using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Aplicacao.Contrato.Consultas
{
    public interface IConsultasListaLojas
    {
        ListaLojas Get(int pagina, int tamanhoPagina);
    }

}
