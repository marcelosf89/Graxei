using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource("FAST.Web.Controls.Images.Loading.gif", "image/gif")]
namespace FAST.Web.Controls
{
    /// <summary>
    /// Classe para WebControl de Button com indicador de progresso
    /// </summary>
    [ToolboxData("<{0}:ProgressButton runat=server></{0}:ProgressButton>")]
    public class ProgressButton : Button
    {
        #region Fields
        private string _progressImagePath;
        private string _progressMessage;
        private int _opacity;
        private string _javaScript;
        private string _div;
        #endregion

        #region Properties
        /// <summary>
        /// Grava e recupera o caminho da imagem de progresso
        /// </summary>
        [Category("Appearance")]
        public string ProgressImagePath
        {
            get { return this._progressImagePath; }
            set { this._progressImagePath = value; }
        }

        /// <summary>
        /// Grava e recupera a mensagem de progresso
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("Carregando...")]
        public string ProgressMessage
        {
            get { return this._progressMessage; }
            set { this._progressMessage = value; }
        }

        /// <summary>
        /// Grava e recupera o alfa do fundo
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(75)]
        public int Opacity
        {
            get { return this._opacity; }
            set { this._opacity = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ProgressButton()
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
            #region JavaScript
            this._javaScript = @"
                <script type='text/javascript' language='javascript'>
                function progress_" + this.ClientID + @"(validationGroup)
                {
                    if (typeof(Page_ClientValidate) == ""function"" && !Page_ClientValidate(validationGroup))
                    {
                        return false;
                    }
                    document.getElementById('progressBackgroundFilter_" + this.ClientID + @"').style.display = """";
                    document.getElementById('processMessage_" + this.ClientID + @"').style.display = """";
                    setTimeout('document.images[""imgCarregando_" + this.ClientID + @"""].src=""" + this.ProgressImagePath + @"""', 200);
                    Img = document.getElementById('imgCarregando_" + this.ClientID + @"');
                    Img.src = """ + this.ProgressImagePath + @"?id="" + Math.random();
                }
                </script>";
            #endregion

            #region div
            this._div = @"
                <div id='progressBackgroundFilter_" + this.ClientID + @"' style='display: none; position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000; filter: alpha(opacity=" + this.Opacity + @"); z-index: 1000;'>
                </div>
                <div id='processMessage_" + this.ClientID + @"' style='display: none; position: fixed; top: 43%; left: 43%; padding: 10px; min-width: 10%; z-index: 1001; background-color: #fff; border: solid 1px #000;'>
                    <img id='imgCarregando_" + this.ClientID + @"' alt='" + this._progressMessage + @"' src='" + this.ProgressImagePath + @"' style='vertical-align: middle' />
                    &nbsp;" + this.ProgressMessage + @"
                </div>";
            #endregion

            this.OnClientClick = string.Format("progress_{0}({1});", this.ClientID, this.ValidationGroup);
            base.OnPreRender(e);
        }

        /// <summary>
        /// Método override para incluir o JavaScript e a div do ProgressButton
        /// </summary>
        /// <param name="writer">Escritor de página</param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine(this._javaScript);
            writer.WriteLine(this._div);
            base.Render(writer);
        }
        #endregion
    }
}
