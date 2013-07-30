using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FAST.Web.Controls
{
    /// <summary>
    /// Classe para WebControl de Panel com Auto Vue
    /// </summary>
    [ToolboxData("<{0}:PaginaInterna runat=server />")]
    public class PaginaInterna : Panel
    {
        #region Propiedade
        /// <summary>
        /// Link do servidor do AutoVue.
        /// </summary>
        [DefaultValue("")]
        [UrlProperty]
        public string TargetURL { get; set; }
        
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public PaginaInterna()
        {

        }
        #endregion

        /// <summary>
        /// Método override para incluir o conteúdo do Panel
        /// </summary>
        /// <param name="writer">Escritor de página</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (base.Enabled)
            {
                string iframe = @"
                <iframe 
                id=""I1"" 
                frameborder=""0"" 
                marginheight=""0""
                name=""I1""
                height=""100%"" width=""100%""
                style=""top: 0px; left: 0px; width:""100%"";
                        height: ""100%""; border-right: 1px solid #7eacb1;
                        border-top: 1px solid #7eacb1; border-left: 1px solid #7eacb1; border-bottom: 1px solid #7eacb1""
                src="""+ this.TargetURL +@""">
                </iframe>
                ";
                writer.WriteLine(iframe);
            }
            base.RenderContents(writer);
        }
    }
}
