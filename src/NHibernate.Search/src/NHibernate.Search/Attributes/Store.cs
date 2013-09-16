using System;
namespace NHibernate.Search.Attributes
{
    public enum Store
    {
        Yes,
        No,
        [Obsolete("This Store has be discontinued (Compress = Yes)")]
        Compress
    }
}