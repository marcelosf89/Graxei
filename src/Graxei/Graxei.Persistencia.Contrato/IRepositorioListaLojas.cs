using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Persistencia.Contrato
{
    public interface IRepositorioListaLojas
    {
        ListaLojas Get(int pagina, int tamanhoPagina, Usuario usuario);
    }
}