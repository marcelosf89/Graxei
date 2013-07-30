using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System;

[assembly: WebResource("FAST.Web.Controls.Images.Loading.gif", "image/gif")]
namespace FAST.Web.Controls
{
    /// <summary>
    /// Classe para WebControl de Button com indicador de progresso
    /// </summary>
    [ToolboxData("<{0}:PainelCarregar runat=server></{0}:PainelCarregar>")]
    public class PainelCarregar : Panel
    {
        private string _div;

        #region Properties
        /// <summary>
        /// Grava e recupera o caminho da imagem de progresso
        /// </summary>
        [Category("Appearance")]
        public string ProgressImagePath
        {
            get;
            set;
        }

        /// <summary>
        /// Grava e recupera o caminho da imagem de progresso
        /// </summary>
        [Category("Appearance")]
        public string ClasseDiv { get; set; }
        /// <summary>
        /// Grava e recupera a mensagem de progresso
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("Carregando...")]
        public string ProgressMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Grava e recupera o alfa do fundo
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(75)]
        public int Opacity
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public PainelCarregar()
        {
            this.initProperties();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Inicializa os campos da classe
        /// </summary>
        private void initProperties()
        {
            this.ProgressImagePath = ((Page)HttpContext.Current.Handler).ClientScript.GetWebResourceUrl(typeof(ProgressButton), @"FAST.Web.Controls.Images.Loading.gif");
            this.ProgressMessage = "Carregando...";
            this.Opacity = 75;
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// Método override para incluir o atributo no ProgressButton
        /// </summary>
        /// <param name="e">Argumento padrão de evento</param>
        protected override void OnPreRender(EventArgs e)
        {
            #region div
            this._div = @"
                <div id='progressBackgroundFilter_" + this.ClientID + @"' style='position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=" + this.Opacity + @"); z-index: 100000000; -moz-opacity:0." + this.Opacity + @";'>
                </div>
                <div id='processMessage_" + this.ClientID + @"' style='position: fixed; top: 43%; left: 43%; padding: 10px; min-width: 10%; z-index: 100000001; background-color: #fff; border: solid 1px #000;' " + (string.IsNullOrEmpty(ClasseDiv) ? string.Empty : ("class='" + ClasseDiv + "'")) + @">
                    <img id='imgCarregando_" + this.ClientID + @"' alt='" + this.ProgressMessage + @"' src='" + this.ProgressImagePath + @"' style='vertical-align: middle' />
                    &nbsp;" + this.ProgressMessage + @"
                </div>";
            #endregion
        }

        /// <summary>
        /// Método override para incluir o JavaScript e a div do ProgressButton
        /// </summary>
        /// <param name="writer">Escritor de página</param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine(this._div);
            base.Render(writer);
        }
        #endregion
    }
}
