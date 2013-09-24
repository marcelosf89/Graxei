﻿using System;
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
        public static string RenderPassword(HtmlHelper html, BootstrapTextBoxModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.htmlFieldName)) return null;

            string combinedHtml = "{0}{1}{2}";

            if (!string.IsNullOrEmpty(model.id)) model.htmlAttributes.Add("id", model.id);
            if (model.tooltipConfiguration != null) model.htmlAttributes.AddRange(model.tooltipConfiguration.ToDictionary());
            // assign placeholder class
            if (!string.IsNullOrEmpty(model.placeholder)) model.htmlAttributes.Add("placeholder", model.placeholder);
            // assign size class
            model.htmlAttributes.AddOrMergeCssClass("class", BootstrapHelper.GetClassForInputSize(model.size));
            // build html for input
            var input = html.Password(model.htmlFieldName, model.value, model.htmlAttributes.FormatHtmlAttributes()).ToHtmlString();

            // account for appendString, prependString, and AppendButtons
            TagBuilder appendPrependContainer = new TagBuilder("div");
            if (!string.IsNullOrEmpty(model.prependString) | !string.IsNullOrEmpty(model.appendString) | model.appendButtons.Count() > 0)
            {
                string addOnPrependString = "";
                string addOnAppendString = "";
                string addOnPrependButtons = "";
                string addOnAppendButtons = "";

                TagBuilder addOn = new TagBuilder("span");
                addOn.AddCssClass("input-group-addon");
                if (!string.IsNullOrEmpty(model.prependString))
                {
                    appendPrependContainer.AddOrMergeCssClass("input-group");
                    addOn.InnerHtml = model.prependString;
                    addOnPrependString = addOn.ToString();
                }
                if (!string.IsNullOrEmpty(model.appendString))
                {
                    appendPrependContainer.AddOrMergeCssClass("input-group");
                    addOn.InnerHtml = model.appendString;
                    addOnAppendString = addOn.ToString();
                }
                if (model.appendButtons.Count() > 0)
                {
                    appendPrependContainer.AddOrMergeCssClass("input-group");
                    model.appendButtons.ForEach(x => addOnAppendButtons += x.ToHtmlString());
                }
                if (model.prependButtons.Count() > 0)
                {
                    appendPrependContainer.AddOrMergeCssClass("input-group");
                    model.prependButtons.ForEach(x => addOnPrependButtons += x.ToHtmlString());
                }

                appendPrependContainer.InnerHtml = addOnPrependButtons + addOnPrependString + "{0}" + addOnAppendString + addOnAppendButtons;
                combinedHtml = appendPrependContainer.ToString(TagRenderMode.Normal) + "{1}{2}";
            }

            string helpText = model.helpText != null ? model.helpText.ToHtmlString() : string.Empty;
            string validationMessage = "";
            if (model.displayValidationMessage)
            {
                string validation = html.ValidationMessage(model.htmlFieldName).ToHtmlString();
                validationMessage = new BootstrapHelpText(validation, model.validationMessageStyle).ToHtmlString();
            }

            return MvcHtmlString.Create(string.Format(combinedHtml, input, validationMessage, helpText)).ToString();
        }
    }
}
