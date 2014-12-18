using System.Security.Cryptography.X509Certificates;
using Graxei.Modelo;
using Graxei.Transversais.ContratosDeDados;
using Graxei.Transversais.ContratosDeDados.Listas;

namespace Graxei.Negocio.Contrato
{
    public interface IServicoListaLojas
    {
        ListaLojas Get(int pagina, int tamanhoPagina);
    }

}