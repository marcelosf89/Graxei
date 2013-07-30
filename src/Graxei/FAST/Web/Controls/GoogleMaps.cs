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
    /// Estrutura de coordenadas
    /// </summary>
    public struct Coordinates
    {
        #region Fields
        private double _latitude;
        private double _longitude;
        #endregion

        #region Properties
        /// <summary>
        /// Grava e recupera a latitude do mapa
        /// </summary>
        public double Latitude
        {
            get { return this._latitude; }
            set { this._latitude = value; }
        }

        /// <summary>
        /// Grava e recupera a longitude
        /// </summary>
        public double Longitude
        {
            get { return this._longitude; }
            set { this._longitude = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor inicializando todos os campos da estrutura
        /// </summary>
        /// <param name="latitude">Latitude da coordenada</param>
        /// <param name="longitude">Longitude da coordenada</param>
        public Coordinates(double latitude, double longitude)
        {
            this._latitude = latitude;
            this._longitude = longitude;
        }
        #endregion
    }

    /// <summary>
    /// Estrutura do ícone do pin
    /// </summary>
    public struct PinIcon
    {
        #region Fields
        private string _iconURL;
        private string _iconShadowURL;
        private Size _iconSize;
        private Size _shadowSize;
        #endregion

        #region Properties
        /// <summary>
        /// Grava e recupera o caminho do ícone
        /// </summary>
        public string IconURL
        {
            get { return this._iconURL; }
            set { this._iconURL = value; }
        }

        /// <summary>
        /// Grava e recupera o caminho do ícone de sombra
        /// </summary>
        public string IconShadowURL
        {
            get { return this._iconShadowURL; }
            set { this._iconShadowURL = value; }
        }

        /// <summary>
        /// Grava e recupera a altura e a largura do ícone
        /// </summary>
        public Size IconSize
        {
            get { return this._iconSize; }
            set { this._iconSize = value; }
        }

        /// <summary>
        /// Grava e recupera a altura e a largura da sombra do ícone
        /// </summary>
        public Size ShadowSize
        {
            get { return this._shadowSize; }
            set { this._shadowSize = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor inicializando todos os campos da estrutura
        /// </summary>
        /// <param name="iconURL">URL da imagem usada como pin</param>
        /// <param name="iconShadowURL">URL da imagem usada como sombra do pin</param>
        /// <param name="iconSize">Estrutura Size com largura e altura</param>
        /// <param name="shadowSize">Estrutura Size com largura e altura</param>
        public PinIcon(string iconURL, string iconShadowURL, Size iconSize, Size shadowSize)
        {
            this._iconURL = iconURL;
            this._iconShadowURL = iconShadowURL;
            this._iconSize = iconSize;
            this._shadowSize = shadowSize;
        }
        #endregion
    }

    /// <summary>
    /// Estrutura do pin
    /// </summary>
    public struct Pin
    {
        #region Fields
        private Coordinates _coordinates;
        private string _title;
        private string _bodyHTML;
        private bool _hasCustomIcon;
        private PinIcon _pinIcon;
        #endregion

        #region Properties
        /// <summary>
        /// Grava e recupera as coordenadas
        /// </summary>
        public Coordinates Coordinates
        {
            get { return this._coordinates; }
            set { this._coordinates = value; }
        }

        /// <summary>
        /// Grava e recupera o título
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }

        /// <summary>
        /// Grava e recupera o HTML usado pela janela de informação
        /// </summary>
        public string BodyHTML
        {
            get { return this._bodyHTML; }
            set { this._bodyHTML = value; }
        }

        /// <summary>
        /// Grava e recupera se tem um ícone customizado
        /// </summary>
        public bool HasCustomIcon
        {
            get { return this._hasCustomIcon; }
            set { this._hasCustomIcon = value; }
        }

        /// <summary>
        /// Grava e recupera o ícone do pin
        /// </summary>
        public PinIcon PinIcon
        {
            get { return this._pinIcon; }
            set { this._pinIcon = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor inicializando todos os campos da estrutura
        /// </summary>
        /// <param name="coordinates">Estrutura de coordenadas</param>
        /// <param name="title">Título do pin</param>
        /// <param name="bodyHTML">HTML usado pela janela de informação do pin</param>
        /// <param name="hasCustomIcon">Se tem um ícone customizado ou se é o padrão do google maps</param>
        /// <param name="pinIcon">Estrutura do ícone do pin</param>
        public Pin(Coordinates coordinates, string title, string bodyHTML, bool hasCustomIcon, PinIcon pinIcon)
        {
            this._coordinates = coordinates;
            this._title = title;
            this._bodyHTML = bodyHTML;
            this._hasCustomIcon = hasCustomIcon;
            this._pinIcon = pinIcon;
        }
        #endregion
    }

    /// <summary>
    /// Classe para WebControl de Panel com Google Maps
    /// </summary>
    [ToolboxData("<{0}:GoogleMaps runat=server></{0}:GoogleMaps>")]
    public class GoogleMaps : Panel
    {
        #region Fields
        private string _apiKey;
        private Coordinates _mapCoordinates;
        private int _mapZoom;
        private IList<Pin> _pins;
        #endregion

        #region Properties
        /// <summary>
        /// Grava e recupera a chave para uso do Google Maps
        /// </summary>
        [Category("Misc")]
        public string APIKey
        {
            get { return this._apiKey; }
            set { this._apiKey = value; }
        }

        /// <summary>
        /// Grava e recupera a latitude do mapa
        /// </summary>
        [Category("Behavior")]
        public double MapLatitude
        {
            get { return this._mapCoordinates.Latitude; }
            set { this._mapCoordinates.Latitude = value; }
        }

        /// <summary>
        /// Grava e recupera a longitude do mapa
        /// </summary>
        [Category("Behavior")]
        public double MapLongitude
        {
            get { return this._mapCoordinates.Longitude; }
            set { this._mapCoordinates.Longitude = value; }
        }

        /// <summary>
        /// Grava e recupera o zoom do mapa
        /// </summary>
        [Category("Behavior")]
        public int MapZoom
        {
            get { return this._mapZoom; }
            set { this._mapZoom = value; }
        }

        /// <summary>
        /// Grava e recupera a lista dos pins
        /// </summary>
        [Category("Misc")]
        public IList<Pin> Pins
        {
            get { return this._pins; }
            set { this._pins = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public GoogleMaps()
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
            this._pins = new List<Pin>();
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// Método override para incluir o JavaScript do Panel
        /// </summary>
        /// <param name="writer">Escritor de página</param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine(@"<script src=""http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=" + this._apiKey + @""" type=""text/javascript""></script>");
            base.Render(writer);
        }

        /// <summary>
        /// Método override para incluir o conteúdo do Panel
        /// </summary>
        /// <param name="writer">Escritor de página</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string latitude = string.Empty;
            string longitude = string.Empty;
            int pinCounter = 1;
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");
            StringBuilder pinsJavaScript = new StringBuilder(@"
                    var options = null;
                    var pinIcon = null;");
            foreach (Pin pin in this.Pins)
            {
                latitude = pin.Coordinates.Latitude.ToString(cultureInfo);
                longitude = pin.Coordinates.Longitude.ToString(cultureInfo);
                string pinIconJavaScript = string.Empty;
                if (pin.HasCustomIcon)
                {
                    pinIconJavaScript = @"
                    pinIcon.image = """ + pin.PinIcon.IconURL + @""";
                    pinIcon.shadow = """ + pin.PinIcon.IconShadowURL + @""";
                    pinIcon.iconSize = new GSize(" + pin.PinIcon.IconSize.Width + ", " + pin.PinIcon.IconSize.Height + @");
                    pinIcon.shadowSize = new GSize(" + pin.PinIcon.ShadowSize.Width + ", " + pin.PinIcon.ShadowSize.Height + @");
                    pinIcon.iconAnchor = new GPoint(" + ((int)pin.PinIcon.IconSize.Width / 2) + ", " + pin.PinIcon.IconSize.Height + @");
                    pinIcon.infoWindowAnchor = new GPoint(" + ((int)pin.PinIcon.IconSize.Width / 2) + @", 0);
                    pinIcon.imageMap = new Array(0,0, " + pin.PinIcon.IconSize.Width + ",0, " + pin.PinIcon.IconSize.Width + "," + pin.PinIcon.IconSize.Height + ", 0," + pin.PinIcon.IconSize.Width + ");";
                }
                pinsJavaScript.Append(@"
                    pinIcon = new GIcon(G_DEFAULT_ICON);
                    " + pinIconJavaScript + @"
                    options = {icon: pinIcon, title: """ + pin.Title + @"""};
                    var marker" + pinCounter + @" = new GMarker(new GLatLng(" + latitude + ", " + longitude + @"), options);
                    GEvent.addListener(marker" + pinCounter + @", 'click', function() {marker" + pinCounter + @".openInfoWindowHtml('" + pin.BodyHTML + @"');});
                    map.addOverlay(marker" + pinCounter + @");");
                pinCounter++;
            }
            latitude = this.MapLatitude.ToString(cultureInfo);
            longitude = this.MapLongitude.ToString(cultureInfo);
            string mapJavaScript = @"
                <script type=""text/javascript"">
                if (GBrowserIsCompatible())
                {
                    var map = new GMap2(document.getElementById(""" + this.ClientID + @"""));
                    map.setCenter(new GLatLng(" + latitude + ", " + longitude + "), " + this._mapZoom + @");
                    " + (this.Pins.Count != 0 ? pinsJavaScript.ToString() : string.Empty) + @"
                    map.setUIToDefault();
                }
                </script>";
            writer.WriteLine(mapJavaScript);
            base.RenderContents(writer);
        }
        #endregion
    }
}
