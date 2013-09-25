using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterBootstrapMVC
{
    public class Width
    {
        public Width(): this(InputWidth.Default)
        {
        }

        public Width(InputWidth width, IDictionary<string, object> htmlAttributes = null)
        {
            this.InputWidth = width;
            this.HtmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
        }

        public InputWidth InputWidth { get; set; }
        public IDictionary<string, object> HtmlAttributes { get; set; }
 
    }
}
