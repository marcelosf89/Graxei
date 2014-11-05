using Graxei.Transversais.ContratosDeDados.Interfaces;

namespace Graxei.Transversais.ContratosDeDados
{
    public class ListaLojasContrato : IItemLista
    {
        public ListaLojasContrato()
        {
        }

        public long Id { get; set; }
        public string Nome { get; set; }
    }
}