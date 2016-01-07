using log4net.Appender;
using System.Configuration;

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
