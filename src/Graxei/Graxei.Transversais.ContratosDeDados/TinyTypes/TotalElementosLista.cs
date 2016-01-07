namespace Graxei.Transversais.ContratosDeDados.TinyTypes
{
    public class TotalElementosLista
    {
        public TotalElementosLista(long valor)
        {
            _valor = valor;
        }

        private long _valor;

        public long Valor
        {
            get { return _valor; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TotalElementosLista) || obj == null)
            {
                return false;
            }

            TotalElementosLista that = (TotalElementosLista)obj;
            return that._valor == this._valor;
        }

        public override int GetHashCode()
        {
            return this._valor.GetHashCode() ^ 3;
        }
    }
}