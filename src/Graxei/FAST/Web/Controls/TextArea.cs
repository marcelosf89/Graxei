using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FAST.Web.Controls
{
    /// <summary>
    /// Classe para WebControl de TextBox com maxLength com contador
    /// </summary>
    [ToolboxData("<{0}:TextArea runat=server></{0}:TextArea>")]
    public class TextArea : TextBox
    {
        #region Fields
        private bool _showCounter;
        private string _counterCssClass;
        private string _counterText;
        #endregion

        #region Properties
        /// <summary>
        /// Grava a classe do counter
        /// </summary>
        [Category("Appearance")]
        public string CounterCssClass
        {
            get { return this._counterCssClass; }
            set { this._counterCssClass = value; }
        }

        /// <summary>
        /// Grava e recupera o texto do counter
        /// </summary>
        [Category("Appearance")]
        public string CounterText
        {
            get { return this._counterText; }
            set { this._counterText = value; }
        }

        /// <summary>
        /// Grava se o controle terá o contador
        /// </summary>
        [Category("Behavior")]
        public bool ShowCounter
        {
            get { return this._showCounter; }
            set { this._showCounter = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public TextArea()
        {
            this.initProperties();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Inicializa as propriedades da classe
        /// </summary>
        private void initProperties()
        {
            this.ShowCounter = true;
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// Método override para incluir o atributo no TextArea
        /// </summary>
        /// <param name="e">Argumento padrão de evento</param>
        protected override void OnPreRender(EventArgs e)
        {
            this.TextMode = TextBoxMode.MultiLine;
            this.Attributes.Add("onKeyUp", "JavaScript: TextAreaMaxLength(this, " + this.MaxLength + ")");
            base.OnPreRender(e);
        }

        /// <summary>
        /// Método override para incluir o JavaScript do TextArea
        /// </summary>
        /// <param name="writer">Escritor de página</param>
        protected override void Render(HtmlTextWriter writer)
        {
            string jsCounter = @"
            var counter = document.getElementById('" + this.ClientID + @"_counter');
            counter.firstChild.data = " + this.MaxLength + " - length;";

            string javaScript = @"
            <script type='text/javascript'>
            function TextAreaMaxLength(textArea, limit)
            {
                var length = textArea.value.length;
                if (length >= limit)
                {
                    length = limit;
                    textArea.value = textArea.value.substring(0, limit);
                }" + (this.ShowCounter ? jsCounter : string.Empty) + @"
            }
            </script>";

            string counterArea = @"
                <span id='" + this.ClientID + @"_text'" + (this.CounterCssClass != null ? " class='" + this.CounterCssClass + "'" : string.Empty) + ">" + this.CounterText + @"
                    <span id='" + this.ClientID + @"_counter'>" + this.MaxLength + @"</span>
                </span>";

            writer.WriteLine(javaScript);
            base.Render(writer);
            if (this.ShowCounter)
            {
                writer.WriteLine(counterArea);
            }
        }
        #endregion
    }
}
