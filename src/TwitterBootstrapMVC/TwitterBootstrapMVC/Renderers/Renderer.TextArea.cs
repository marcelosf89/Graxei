using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TwitterBootstrapMVC.ControlModels;
using TwitterBootstrapMVC.Controls;
using TwitterBootstrapMVC.Infrastructure;
using TwitterBootstrapMVC.TypeExtensions;

namespace TwitterBootstrapMVC.Renderers
{
    internal static partial class Renderer
    {
        public static string RenderTextArea(HtmlHelper html, BootstrapTextAreaModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.htmlFieldName)) return null;

            string validationMessage = "";
            if (model.displayValidationMessage)
            {
                string validation = html.ValidationMessage(model.htmlFieldName).ToHtmlString();
                validationMessage = new BootstrapHelpText(validation, model.validationMessageStyle).ToHtmlString();
            }
            if (!string.IsNullOrEmpty(model.id)) model.htmlAttributes.AddOrReplace("id", model.id);
            if (model.tooltipConfiguration != null) model.htmlAttributes.AddRange(model.tooltipConfiguration.ToDictionary());

            TagBuilder containerDiv = new TagBuilder("div");
            containerDiv.AddOrMergeCssClass(BootstrapHelper.GetClassForInputWidth(model.width.InputWidth));
            foreach (string key in model.width.HtmlAttributes.Keys)
            {
                containerDiv.MergeAttribute(key, (string)model.width.HtmlAttributes[key]);
            }
            containerDiv.InnerHtml = html.TextArea(model.htmlFieldName, model.value, model.rows, model.columns, model.htmlAttributes.FormatHtmlAttributes()).ToHtmlString() + validationMessage;

            return containerDiv.ToString();
        }
    }
}
