using System;

namespace Graxei.Transversais.Comum.Data
{
    public class DataSistemaPadrao : IDataSistema
    {
        public DateTime Agora
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
