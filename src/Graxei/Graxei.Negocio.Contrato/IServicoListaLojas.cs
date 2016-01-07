using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoListaLojas
    {
        ListaLojas Get(int pagina, int tamanhoPagina);
    }

}