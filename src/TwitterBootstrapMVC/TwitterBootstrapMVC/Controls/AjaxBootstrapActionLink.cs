using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using TwitterBootstrapMVC.ControlInterfaces;
using TwitterBootstrapMVC.ControlModels;
using TwitterBootstrapMVC.Infrastructure;
using TwitterBootstrapMVC.TypeExtensions;

namespace TwitterBootstrapMVC.Controls
{
    public class AjaxBootstrapActionLink : BootstrapButtonBase<AjaxBootstrapActionLink>
    {
        private AjaxHelper ajax;
        private AjaxOptions _result;
        private string _routeName;
        private string _actionName;
        private string _controllerName;
        private string _protocol;
        private string _hostName;
        private string _fragment;
        private RouteValueDictionary _routeValues;

        public AjaxBootstrapActionLink(AjaxHelper ajax, string linkText, AjaxOptions result)
            : base("")
        {
            this.ajax = ajax;
            this._model.text = linkText;
            this._result = result ?? new AjaxOptions();
            this._model.size = ButtonSize.Default;
            this._model.style = ButtonStyle.Default;
        }

        public AjaxBootstrapActionLink(AjaxHelper ajax, string linkText, string actionName, AjaxOptions result = null)
            : base("")
        {
            this.ajax = ajax;
            this._model.text = linkText;
            this._actionName = actionName;
            this._model.size = ButtonSize.Default;
            this._model.style = ButtonStyle.Default;
            this._result = result ?? new AjaxOptions();
        }

        public AjaxBootstrapActionLink(AjaxHelper ajax, string linkText, string actionName, string controllerName, AjaxOptions result = null)
            : base("")
        {
            this.ajax = ajax;
            this._model.text = linkText;
            this._actionName = actionName;
            this._controllerName = controllerName;
            this._model.size = ButtonSize.Default;
            this._model.style = ButtonStyle.Default;
            this._result = result ?? new AjaxOptions();
        }

        public AjaxBootstrapActionLink Id(string id)
        {
            this._model.id = id;
            return this;
        }

        public AjaxBootstrapActionLink Protocol(string protocol)
        {
            this._protocol = protocol;
            return this;
        }

        public AjaxBootstrapActionLink HtmlAttributes(IDictionary<string, object> htmlAttributes)
        {
            this._model.htmlAttributes = htmlAttributes;
            return this;
        }

        public AjaxBootstrapActionLink HostName(string hostName)
        {
            this._hostName = hostName;
            return this;
        }

        public AjaxBootstrapActionLink Fragment(string fragment)
        {
            this._fragment = fragment;
            return this;
        }

        public AjaxBootstrapActionLink RouteName(string routeName)
        {
            this._routeName = routeName;
            return this;
        }

        public AjaxBootstrapActionLink RouteValues(RouteValueDictionary routeValues)
        {
            this._routeValues = routeValues;
            return this;
        }

        public AjaxBootstrapActionLink RouteValues(object routeValues)
        {
            this._routeValues = HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues);
            return this;
        }

        public AjaxBootstrapActionLink Disabled()
        {
            this._model.disabled = true;
            return this;
        }

        public AjaxBootstrapActionLink DropDownToggle()
        {
            this._model.isDropDownToggle = true;
            return this;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToHtmlString()
        {
            var mergedHtmlAttributes = _model.htmlAttributes;
            if(!string.IsNullOrEmpty(_model.id)) mergedHtmlAttributes.AddIfNotExist("id", _model.id);

            mergedHtmlAttributes.AddOrMergeCssClass("class", BootstrapHelper.GetClassForButtonSize(_model.size));

            if (_model.buttonBlock) mergedHtmlAttributes.AddOrMergeCssClass("class", "btn-block");
            if (_model.isDropDownToggle)
            {
                mergedHtmlAttributes.AddOrMergeCssClass("class", "dropdown-toggle");
                mergedHtmlAttributes.AddIfNotExist("data-toggle", "dropdown");
            }
            if (_model.disabled) mergedHtmlAttributes.AddOrMergeCssClass("class", "disabled");

            var input = string.Empty;
            if (_model.iconPrepend != Icons._not_set || _model.iconAppend != Icons._not_set || !string.IsNullOrEmpty(_model.iconPrependCustomClass) || !string.IsNullOrEmpty(_model.iconAppendCustomClass))
            {
                
                string iPrependString = string.Empty;
                string iAppendString = string.Empty;
                if (_model.iconPrepend != Icons._not_set) iPrependString = new BootstrapIcon(_model.iconPrepend, _model.iconPrependIsWhite).ToHtmlString();
                if (_model.iconAppend != Icons._not_set) iAppendString = new BootstrapIcon(_model.iconAppend, _model.iconAppendIsWhite).ToHtmlString();
                if (!string.IsNullOrEmpty(_model.iconPrependCustomClass))
                {
                    var i = new TagBuilder("i");
                    i.AddCssClass(_model.iconPrependCustomClass);
                    iPrependString = i.ToString(TagRenderMode.Normal);
                }
                if (!string.IsNullOrEmpty(_model.iconAppendCustomClass))
                {
                    var i = new TagBuilder("i");
                    i.AddCssClass(_model.iconAppendCustomClass);
                    iPrependString = i.ToString(TagRenderMode.Normal);
                }

                string combined = 
                    iPrependString +
                    (!string.IsNullOrEmpty(iPrependString) && (!string.IsNullOrEmpty(_model.text) || !string.IsNullOrEmpty(iAppendString)) ? " " : "") +
                    _model.text +
                    (!string.IsNullOrEmpty(iAppendString) && (!string.IsNullOrEmpty(_model.text) || !string.IsNullOrEmpty(iPrependString)) ? " " : "") +
                    iAppendString;

                string holder = Guid.NewGuid().ToString();
                input =
                    ajax.ActionLink(holder, this._actionName, this._controllerName, _protocol, _hostName, _fragment, this._routeValues, _result, mergedHtmlAttributes).ToHtmlString();
                input = input.Replace(holder, combined);
            }
            else
            {
                input = ajax.ActionLink(this._model.text, this._actionName, this._controllerName, _protocol, _hostName, _fragment, this._routeValues, _result, mergedHtmlAttributes).ToHtmlString();
            }
            
            return MvcHtmlString.Create(input).ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return ToHtmlString(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); }
    }
}
