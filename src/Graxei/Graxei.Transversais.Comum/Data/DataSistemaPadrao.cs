using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum.Data
{
    public class DataSistemaPadrao : IDataSistema
    {
        public DateTime Agora
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
