﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TwitterBootstrapMVC.ControlInterfaces;
using TwitterBootstrapMVC.ControlModels;
using TwitterBootstrapMVC.Controls;
using TwitterBootstrapMVC.Infrastructure;
using TwitterBootstrapMVC.TypeExtensions;

namespace TwitterBootstrapMVC.Renderers
{
    internal static partial class Renderer
    {
        public static string TypeHead(HtmlHelper html, BootstrapTypeHeadModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.htmlFieldName)) return null;

            string combinedHtml = "{0}{1}{2}";

            if (!string.IsNullOrEmpty(model.id)) model.htmlAttributes.Add("id", model.id);
            if (model.tooltipConfiguration != null) model.htmlAttributes.AddRange(model.tooltipConfiguration.ToDictionary());
            // assign placeholder class
            if (!string.IsNullOrEmpty(model.placeholder)) model.htmlAttributes.Add("placeholder", model.placeholder);
            // assign size class
            model.htmlAttributes.AddOrMergeCssClass("class", BootstrapHelper.GetClassForInputSize(model.size));

            model.htmlAttributes.Add("data-provide", "typeahead");
            // build html for input
            var input = html.TextBox(model.htmlFieldName, model.value, model.format, model.htmlAttributes.FormatHtmlAttributes()).ToHtmlString();

            // account for appendString, prependString, and AppendButtons
            if (!string.IsNullOrEmpty(model.prependString) | !string.IsNullOrEmpty(model.appendString) | model.appendButtons.Count() > 0)
            {
                TagBuilder appendPrependContainer = new TagBuilder("div");
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

            String url = new UrlHelper(html.ViewContext.RequestContext).Action(model.actionAutoComplete);
            if (!String.IsNullOrEmpty(model.controllerAutoComplete))
            {
                url = new UrlHelper(html.ViewContext.RequestContext).Action(model.actionAutoComplete, model.controllerAutoComplete);
            }

            if (String.IsNullOrEmpty(model.id))
            {
                model.id = model.htmlFieldName.Replace(".", "_");
            }
            String script = @"<script>
                $(function () {
                    $('#" + model.id + @"').typeahead({
                        source: function (term, process) {
                        var url = '" + url + @"'; 
                        return $.getJSON(url, { term: term }, function (data) {
                        return process(data);
                            });
                        }
                    });
                })
            </script>";


            TagBuilder containerDiv = new TagBuilder("div");
            containerDiv.AddOrMergeCssClass(BootstrapHelper.GetClassForInputWidth(model.width.InputWidth));
            foreach (string key in model.width.HtmlAttributes.Keys)
            {
                containerDiv.MergeAttribute(key, (string)model.width.HtmlAttributes[key]);
            }
            containerDiv.InnerHtml = MvcHtmlString.Create(string.Format(combinedHtml, input, validationMessage, helpText) + script).ToString();

            return containerDiv.ToString();
        }
    }
}
