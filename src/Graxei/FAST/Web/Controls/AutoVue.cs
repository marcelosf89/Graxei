using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FAST.Web.Controls
{
    public enum ONINITOptions
    {
        /// <summary>
        /// Inicia a applet
        /// </summary>
        InitApplet,
        /// <summary>
        /// Seta o arquivo no JVUE
        /// </summary>
        SetarArquivo
    }

    public enum LocalCargaArquivoOptions
    {
        /// <summary>
        /// Maquina local do acesso do client.
        /// </summary>
        Local,
        /// <summary>
        /// Servido da rede local.
        /// </summary>
        ServidorLocal,
        /// <summary>
        /// FPT sem segurançã.
        /// </summary>
        FTP,
        /// <summary>
        /// FPT com o usuario e senha obrigatorio.
        /// </summary>
        FTPSec,
        /// <summary>
        /// Arquivo localizado em uma pagina web.
        /// </summary>
        Web
    }

    public struct ONINIT
    {
        #region Propiedade
        /// <summary>
        /// Opição do parametro ONINIT
        /// </summary>
        private ONINITOptions _opcao;
        public ONINITOptions opcao { get { return this._opcao; } set { this._opcao = value; } }
        #endregion

        #region Construtor
        public ONINIT(ONINITOptions opcao)
        {
            this._opcao = opcao;
        }
        #endregion
    }
    /// <summary>
    /// Classe para WebControl de Panel com Auto Vue
    /// </summary>
    [ToolboxData("<{0}:AutoVue runat=server></{0}:AutoVue>")]
    public class AutoVue : Panel
    {
        #region Propiedade
        /// <summary>
        /// Link do servidor do AutoVue.
        /// </summary>
        [DefaultValue("")]
        public string Servidor { get; set; }
        /// <summary>
        /// Local aonde estao as bibliotecas "jvue.jar,jogl.jar,gluegen-rt.jar".
        /// </summary>
        [DefaultValue("")]
        public string LocalBibliotecaJVUE { get; set; }
        /// <summary>
        /// Usuario que ira assinar o markup.
        /// </summary>
        [DefaultValue("Sistema")]
        public string Usuario { get; set; }
        /// <summary>
        /// Largura da Applet
        /// </summary>
        [DefaultValue("100%")]
        public string WidthApplet { get; set; }
        /// <summary>
        /// Altura da Applet
        /// </summary>
        [DefaultValue("100%")]
        public string HeightApplet { get; set; }
        /// <summary>
        /// Listagem dos scripts ao iniciar o AutoVue
        /// </summary>
        [Category("Misc")]
        public List<ONINIT> ONINITs { get; set; }
        /// <summary>
        /// Caminho do arquivo que ira ser carregado no AutoVue
        /// </summary>
        [DefaultValue("")]
        public string Arquivo { get; set; }
        /// <summary>
        /// Aonde o arquivo se encontra ( FTP, FTPSec, Web, Local ou Servidor Local )
        /// </summary>
        [DefaultValue(LocalCargaArquivoOptions.Local)]
        public LocalCargaArquivoOptions LocalCargaArquivo { get; set; }
        /// <summary>
        /// Usuario que ira se logar caso a opção do LocalCargaArquivo for FTPSec
        /// </summary>
        [DefaultValue("")]
        public string FTPUsuario { get; set; }
        /// <summary>
        /// Senha que ira se logar caso a opção do LocalCargaArquivo for FTPSec
        /// </summary>
        [DefaultValue("")]
        public string FTPSenha { get; set; }

        [DefaultValue(false)]
        public bool Embedded { get; set; }

        [DefaultValue(false)]
        public bool VerBose { get; set; }
        #endregion

        #region Construtor
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public AutoVue()
        {

        }
        #endregion

        #region Metodos Privados

        private string escreverScriptONINIT()
        {
            string script = string.Empty;

            foreach (ONINIT oninit in ONINITs)
            {
                switch (oninit.opcao)
                {
                    case ONINITOptions.InitApplet:
                        script += @"
                        function " + this.nomeFunctionONINIT(oninit) + @"
                        {
	                        closePopupDlg();
	                        window.jVueLoaded = true;
	                        window.status = 'Applet carregada.';
                        }";
                        break;
                    case ONINITOptions.SetarArquivo:
                        script += @"
                        function " +this.nomeFunctionONINIT(oninit) +@"
                        {
	                        if (window.jVueLoaded) {
		                        // Carregar o arquivo separado na thread.
		                        window.document.applets['JVue'].setFileThreaded(fileURL);
	                        } else {
		                        alert('Por favor espere a applet carregar...');
	                        }
                        }";
                        break;
                    default:
                        break;
                }
            }
            return script;
        }

        private string nomeFunctionONINIT(ONINIT oninit)
        {
            string funcao = string.Empty;

                switch (oninit.opcao)
                {
                    case ONINITOptions.InitApplet:
                        funcao += @"onAppletInit()";
                        break;
                    case ONINITOptions.SetarArquivo:
                        funcao += @"setFile(fileURL)";
                        break;
                    default:
                        break;
                }
            return funcao;
        }

        private string escreveTagLocalCargaArquivo(LocalCargaArquivoOptions localArquivo)
        {
            string tagLocalCargaArquivo = string.Empty;

            switch (localArquivo)
            {
                case LocalCargaArquivoOptions.FTP:
                    tagLocalCargaArquivo += @"ftp://";
                    break;
                case LocalCargaArquivoOptions.Local:
                    tagLocalCargaArquivo += @"upload://";
                    break;
                case LocalCargaArquivoOptions.ServidorLocal:
                    tagLocalCargaArquivo += @"server://";
                    break;
                case LocalCargaArquivoOptions.FTPSec:
                    tagLocalCargaArquivo += string.Format(@"ftp://{0}:{1}@", this.FTPUsuario, this.FTPSenha);
                    break;
                case LocalCargaArquivoOptions.Web:
                    tagLocalCargaArquivo += @"http://";
                    break;
                default:
                    break;
            }
            return tagLocalCargaArquivo;
        }

        #endregion

        #region Metodos Override
        /// <summary>
        /// Método override para incluir o JavaScript do Panel
        /// </summary>
        /// <param name="writer">Escritor de página</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (base.Enabled)
            {
                string script =
                    @"<script> 
 
                    <!-- Hide script from old browsers
 
                    /*
                    **  Create a popup window to tell the user to wait for the applet load
                    */
                    window.msgLoadingApplet =
	                    '<html><title>Loading Applet<' + '/title>' +
	                    '<body bgcolor=""#cccccc"" style=""padding:0; margin:0;""><center><br/><span style=""font-family:Arial,Helvetica,sans-serif;font-size:10pt;"">Porfavor esperar a applet carregar...</span><' + '/center><' + '/body><' +
                        '/html>';
 
                    window.msgConnecting =
	                    '<html><title>Loading Applet<' + '/title>' +
	                    '<body bgcolor=""#cccccc"" style=""padding:0; margin:0;""><center><br/><span style=""font-family:Arial,Helvetica,sans-serif;font-size:10pt;"">Conectando o servidor...</span><' + '/center><' + '/body><' +
	                    '/html>';
 
                    window.popupDlg =  (document.all && window.print) ?
	                    /*ie5*/
	                    window.showModelessDialog('frmHeading.html', 'popupDlg', 'dialogHeight:50px;dialogWidth:200px;resizable:1;help:0;scroll:0;status:0') :
	                    /*not-ie5*/
	                    window.showModalDialog('', 'popupDlg', 'height=50,width=200,resizable=1,titlebar=0,scrollbars=0,status=0,menubar=0,toolbar=0');
 
                    window.popupDlg.document.open();
                    window.popupDlg.document.write(window.msgLoadingApplet);
                    window.popupDlg.document.close();

                    function closePopupDlg()
                    {
	                    if (window.popupDlg != null && !window.popupDlg.closed) {
		                    window.popupDlg.close();
	                    }
	                    window.popupDlg = null;
	                    window.status = '';
                    }
 
                    function onBodyLoad()
                    {
	                    if (window.popupDlg != null && !window.popupDlg.closed) {
		                    window.popupDlg.document.open();
		                    window.popupDlg.document.write(window.msgConnecting);
		                    window.popupDlg.document.close();
		                    setTimeout('closePopupDlg()', 10000);
		                    window.status = 'Conectando o servidor...';
	                    }
                    }
";

                script += this.escreverScriptONINIT();

                script += @"
                        // -->
                        </script>";


                writer.WriteLine(script);
            }
            base.Render(writer);
        }

        /// <summary>
        /// Método override para incluir o conteúdo do Panel
        /// </summary>
        /// <param name="writer">Escritor de página</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (base.Enabled)
            {
                string applet = @"
                <APPLET
                    NAME=""JVue"" 
                    CODE=""com.cimmetry.jvue.JVue.class""
                    CODEBASE=""" + this.LocalBibliotecaJVUE + @"""
                    ARCHIVE=""jvue.jar,jogl.jar,gluegen-rt.jar""

                      HSPACE=""0"" VSPACE=""0""
                    WIDTH=""" + this.WidthApplet + @""" HEIGHT=""" + this.HeightApplet + @""" MAYSCRIPT>
 
                    <PARAM NAME=""EMBEDDED""     VALUE=""" + this.Embedded + @""">
                    <PARAM NAME=""VERBOSE""      VALUE=""" + this.VerBose + @""">
                    <PARAM NAME=""GUIFILE""      VALUE=""newui.gui"">
 
                    <!-- Optional: To call a Javascript function after the applet has initialized -->";

                if (ONINITs != null)
                {
                    foreach (ONINIT oninit in ONINITs)
                    {
                        applet += @"<PARAM NAME=""ONINIT"" VALUE=""" + this.nomeFunctionONINIT(oninit) + @""">";
                    }
                }
                applet += @"
                    <!-- SETAR O FILENAME QUE VAI INICIAR O AUTOVUE -->
                    <PARAM NAME=""FILENAME"" VALUE=""" + escreveTagLocalCargaArquivo(this.LocalCargaArquivo) + this.Arquivo + @""">

                    <!-- SETAR O USUARIO QUE IRA ESCREVER O COMENTARIO -->
                    <PARAM NAME=""USERNAME"" VALUE=""" + this.Usuario + @""">    
    
                    <!-- SETA O O SERVIDOR DO JVUE -->
                    <PARAM NAME=""JVUESERVER"" VALUE=""" + this.Servidor + @""">
 
                    <p><b>O Java run time precisa estar instalado na sua mequina!</b></p>
                 </APPLET>
                ";
                writer.WriteLine(applet);
            }
            base.RenderContents(writer);
        }
        protected override void OnLoad(System.EventArgs e)
        {
            if (base.Enabled)
            {

            }
            base.OnLoad(e);
        }

        protected override void OnUnload(System.EventArgs e)
        {
            if (base.Enabled)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "closePopupDlg()", true);
            }
            base.OnUnload(e);
        }
        #endregion
    }
}
