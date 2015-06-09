using log4net.Appender;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum.LogAplicacao.Log4Net
{
    public class WebAppAdoNetAppender : AdoNetAppender
    {
        public new string ConnectionString
        {
            get { return base.ConnectionString; }
            set { base.ConnectionString = ConfigurationManager.ConnectionStrings["graxei"].ToString(); }
        }
    }
}
