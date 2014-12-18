using Graxei.Transversais.ContratosDeDados.Interfaces;

namespace Graxei.Transversais.ContratosDeDados.Listas
{
    public class ListaLojasContrato : IItemLista
    {
        public ListaLojasContrato()
        {
        }

        public long Id { get; set; }
        public string Nome { get; set; }
        public string NomePlano { get; set; }
    }
}