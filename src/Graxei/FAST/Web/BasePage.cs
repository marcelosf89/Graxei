using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FAST.Log;

namespace FAST.Web
{
    /// <summary>
    /// Classe básica para páginas
    /// </summary>
    public class BasePage : Page
    {
        #region Protected Methods
        /// <summary>
        /// Bloqueia o botão de ser apertado mais de uma vez
        /// </summary>
        /// <param name="button">Botão para colocar o script</param>
        /// <exception cref="ArgumentException">Parâmetro deve implementar IButtonControl</exception>
        protected void SetDoubleClickBlock(WebControl button)
        {
            this.SetDoubleClickBlock(button, null);
        }

        /// <summary>
        /// Bloqueia o botão de ser apertado mais de uma vez
        /// </summary>
        /// <param name="button">Botão para colocar o script</param>
        /// <param name="disabledButtonText">Texto para ser colocado no botão quando for desabilitado</param>
        /// <exception cref="ArgumentException">Parâmetro deve implementar IButtonControl</exception>
        protected void SetDoubleClickBlock(WebControl button, string disabledButtonText)
        {
            IButtonControl iButtonControl = null;
            try { iButtonControl = (IButtonControl)button; }
            catch { throw new ArgumentException("Parâmetro não pode ser convertido em IButtonControl", "button"); }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("if (typeof(Page_ClientValidate) == 'function' && !Page_ClientValidate('" + iButtonControl.ValidationGroup + "')) { return false; } ");
            if (!string.IsNullOrEmpty(disabledButtonText))
            {
                stringBuilder.Append(string.Format("this.value = '{0}'; ", disabledButtonText));
            }
            stringBuilder.Append("this.disabled = true; ");
            stringBuilder.Append(this.Page.ClientScript.GetPostBackEventReference(button, string.Empty));
            stringBuilder.Append(";");
            button.Attributes.Add("onclick", stringBuilder.ToString());
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// Evento de erro da página que herda esta classe
        /// </summary>
        /// <param name="e">Argumento do evento</param>
        protected override void OnError(EventArgs e)
        {
            Exception exception = HttpContext.Current.Server.GetLastError();
            string path = HttpContext.Current.Request.Path;
            string message = string.Format("Unhandled Exception - '{0}'", path);
            ExceptionLogger.Instance.LogException(Level.Error, message, exception);
            HttpContext.Current.Server.ClearError();
            base.OnError(e);
        }
        #endregion
    }
}
