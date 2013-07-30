using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FAST.Web.Controls
{
    /// <summary>
    /// Opções de máscaras padronizadas
    /// </summary>
    public enum MaskOptions
    {
        /// <summary>
        /// Mascara definida por uma string
        /// </summary>
        Custom = 0,
        /// <summary>
        /// CEP no formato #####-###
        /// </summary>
        CEP,
        /// <summary>
        /// CNPJ no formato ##.###.###/####-##
        /// </summary>
        CNPJ,
        /// <summary>
        /// CPF no formato ###.###.###-##
        /// </summary>
        CPF,
        /// <summary>
        /// Data no formato ##/##/####
        /// </summary>
        Date,
        /// <summary>
        /// Data e hora no formato ##/##/#### ##:##:##
        /// </summary>
        DateTime,
        /// <summary>
        /// Moeda no formato #.###,##
        /// </summary>
        Money,
        /// <summary>
        /// Apenas números
        /// </summary>
        OnlyNumbers,
        /// <summary>
        /// Porcentagem no formato #.###,##%
        /// </summary>
        Percent,
        /// <summary>
        /// Telefone no formato ####-####
        /// </summary>
        PhoneNumber,
        /// <summary>
        /// Telefone com área no formato (##) ####-####
        /// </summary>
        PhoneNumberArea,
        /// <summary>
        /// Hora no formato ##:##:##
        /// </summary>
        Time
    }

    /// <summary>
    /// Classe para WebControl de TextBox com máscara
    /// </summary>
    [ToolboxData("<{0}:MaskedTextBox runat=server></{0}:MaskedTextBox>")]
    public class MaskedTextBox : TextBox
    {
        #region Fields
        private MaskOptions _mask;
        private string _customMask;
        private string _jsOnlyNumbers;
        private string _jsMaskedField;
        private string _jsNumberMask;
        #endregion

        #region Properties
        /// <summary>
        /// Grava e recupera o CustomMask
        /// </summary>
        [Category("Behavior")]
        public string CustomMask
        {
            get { return this._customMask; }
            set { this._customMask = value; }
        }

        /// <summary>
        /// Grava e recupera a máscara
        /// </summary>
        [Category("Behavior")]
        public MaskOptions Mask
        {
            get { return this._mask; }
            set
            {
                this._mask = value;
                switch (value)
                {
                    case MaskOptions.CEP:
                        this._customMask = "00000-000";
                        break;
                    case MaskOptions.CNPJ:
                        this._customMask = "00.000.000/0000-00";
                        break;
                    case MaskOptions.CPF:
                        this._customMask = "000.000.000-00";
                        break;
                    case MaskOptions.Date:
                        this._customMask = "00/00/0000";
                        break;
                    case MaskOptions.DateTime:
                        this._customMask = "00/00/0000 00:00:00";
                        break;
                    case MaskOptions.PhoneNumber:
                        this._customMask = "0000-0000";
                        break;
                    case MaskOptions.PhoneNumberArea:
                        this._customMask = "(00) 0000-0000";
                        break;
                    case MaskOptions.Time:
                        this._customMask = "00:00:00";
                        break;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public MaskedTextBox()
        {
            this.initFields();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Inicializa os atributos da classe
        /// </summary>
        private void initFields()
        {
            #region OnlyNumbers
            this._jsOnlyNumbers = @"
                <script type='text/javascript'>
                function ActuOnlyNumbers(e)
                {
                    var keyNum = (window.event) ? window.event.keyCode : e.which;
                    var keyChar = String.fromCharCode(keyNum);
                    if (!/[\x00-\x1F\x7F]/.test(keyChar) && !/\d/.test(keyChar))
                    {
                        if (window.event) { window.event.returnValue = false; }
                        return false;
                    }
                }
                </script>";
            #endregion

            #region MaskedField
            this._jsMaskedField = @"
                <script type='text/javascript'>
                function ActuMaskedField(field, mask, e)
                {
                    var keyNum = (window.event) ? window.event.keyCode : e.which;
                    var keyChar = String.fromCharCode(keyNum);
                    if (/[\x00-\x1F\x7F]/.test(keyChar))
                    {
                        return true;
                    }
                    var maskExp = /[^0a#]/g;
                    var fieldExp = /[^a-zA-Z\d]/g;
                    var tempMask = mask.replace(maskExp, '');
                    var tempValue = field.value.replace(fieldExp, '');
                    switch (tempMask.charAt(tempValue.length))
                    {
                        case '0':
                            if (!/\d/.test(keyChar))
                            {
                                if (window.event) { window.event.returnValue = false; }
                                return false;
                            }
                            break;
                        case 'a':
                            if (!/[a-zA-Z]/.test(keyChar))
                            {
                                if (window.event) { window.event.returnValue = false; }
                                return false;
                            }
                            break;
                        case '#':
                            if (!/[a-zA-Z\d]/.test(keyChar))
                            {
                                if (window.event) { window.event.returnValue = false; }
                                return false;
                            }
                            break;
                    }
                    var newValue = '';
                    var newLength = tempValue.length + 1;
                    for (it1 = 0, it2 = 0; it1 < newLength; it1++)
                    {
                        var maskChar = mask.charAt(it1);
                        var valueChar = tempValue.charAt(it2);
                        if (/[^0a#]/.test(maskChar))
                        {
                            newValue += maskChar;
                            newLength++;
                        }
                        else if (valueChar != '')
                        {
                            newValue += valueChar;
                            it2++;
                        }
                    }
                    field.value = newValue;
                    if (tempValue.length >= tempMask.length)
                    {
                        if (window.event) { window.event.returnValue = false; }
                        return false;
                    }
                    return true;
                }
                </script>";
            #endregion

            #region NumberMask
            this._jsNumberMask = @"
                <script type='text/javascript'>
                function ActuNumberMask(field, percent, e)
                {
                    var keyNum = (window.event) ? window.event.keyCode : e.which;
                    var keyChar = String.fromCharCode(keyNum);
                    if(keyNum == 9 || keyNum == 13)
                    {
                        return true;
                    }
                    else if (!/\d/.test(keyChar) && keyNum != 8)
                    {
                        if (window.event) { window.event.returnValue = false; }
                        return false;
                    }
                    var value = field.value.replace(/[\.\,\%]/g, '').replace(/^0*/, '');
                    value = ((keyNum == 8) ? value.substring(0, value.length - 1) : value + keyChar).split('').reverse().join('');
                    var length = value.length > 3 ? value.length : 3;
                    var temp = '';
                    for (it = 0; it < length; it++)
                    {
                        temp += (it == 2) ? ',' : (it > 4 && (it - 2) % 3 == 0) ? '.' : '';
                        temp += (value.charAt(it) != '') ? value.charAt(it) : 0;
                    }
                    field.value = temp.split('').reverse().join('');
                    field.value += percent ? '%' : '';
                    if (window.event) { window.event.returnValue = false; }
                    return false;
                }
                </script>";
            #endregion
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// Método override para incluir o atributo no MaskedTextBox
        /// </summary>
        /// <param name="e">Argumento padrão de evento</param>
        protected override void OnPreRender(EventArgs e)
        {
            this.TextMode = TextBoxMode.SingleLine;
            switch (this._mask)
            {
                case MaskOptions.OnlyNumbers:
                    this.Attributes.Add("onKeyPress", "JavaScript: return ActuOnlyNumbers(event)");
                    break;
                case MaskOptions.Money:
                    this.Style.Add("text-align", "right");
                    this.Text = "0,00";
                    this.Attributes.Add("onKeyPress", "JavaScript: return ActuNumberMask(this, false, event)");
                    break;
                case MaskOptions.Percent:
                    this.Style.Add("text-align", "right");
                    this.Text = "0,00%";
                    this.Attributes.Add("onKeyDown", "JavaScript: return ActuNumberMask(this, true, event)");
                    break;
                default:
                    this.Attributes.Add("onKeyPress", "JavaScript: return ActuMaskedField(this, '" + this._customMask + "', event)");
                    break;
            }
            base.OnPreRender(e);
        }

        /// <summary>
        /// Método override para incluir o JavaScript do MaskedTextBox
        /// </summary>
        /// <param name="writer">Escritor de página</param>
        protected override void Render(HtmlTextWriter writer)
        {
            switch (this._mask)
            {
                case MaskOptions.Money:
                    writer.WriteLine(this._jsNumberMask);
                    break;
                case MaskOptions.Percent:
                    writer.WriteLine(this._jsNumberMask);
                    break;
                case MaskOptions.OnlyNumbers:
                    writer.WriteLine(this._jsOnlyNumbers);
                    break;
                default:
                    writer.WriteLine(this._jsMaskedField);
                    break;
            }
            base.Render(writer);
        }
        #endregion
    }
}
