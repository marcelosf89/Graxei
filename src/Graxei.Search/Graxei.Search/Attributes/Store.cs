using System;
namespace Graxei.Search.Attributes
{
    public enum Store
    {
        Yes,
        No,
        [Obsolete("This Store has be discontinued (Compress = Yes)")]
        Compress
    }
}