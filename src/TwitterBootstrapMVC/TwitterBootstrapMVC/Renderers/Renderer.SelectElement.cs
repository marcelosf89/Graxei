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
using TwitterBootstrapMVC.Infrastructure.Enums;
using TwitterBootstrapMVC.TypeExtensions;

namespace TwitterBootstrapMVC.Renderers
{
    internal static partial class Renderer
    {
        public static string RenderSelectElement(HtmlHelper html, BootstrapSelectElementModel model, BootstrapInputType inputType)
        {
            if (model == null || string.IsNullOrEmpty(model.htmlFieldName) || model.selectList == null) return null;

            string combinedHtml = "{0}{1}{2}";
            if (model.selectedValue != null)
            {
                foreach (var item in model.selectList)
                {
                    if (item.Value == model.selectedValue.ToString())
                        item.Selected = true;
                }
            }
            if (model.tooltipConfiguration != null) model.htmlAttributes.AddRange(model.tooltipConfiguration.ToDictionary());
            if (!string.IsNullOrEmpty(model.id)) model.htmlAttributes.AddOrReplace("id", model.id);

            // assign size class
            model.htmlAttributes.AddOrMergeCssClass("class", BootstrapHelper.GetClassForInputSize(model.size));

            // build html for input
            string input = string.Empty;

            if (inputType == BootstrapInputType.DropDownList)
                input = html.DropDownList(model.htmlFieldName, model.selectList, model.optionLabel, model.htmlAttributes.FormatHtmlAttributes()).ToHtmlString();

            if (inputType == BootstrapInputType.ListBox)
                input = html.ListBox(model.htmlFieldName, model.selectList, model.htmlAttributes.FormatHtmlAttributes()).ToHtmlString();

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
                    ((List<BootstrapButton>)model.appendButtons).ForEach(x => addOnAppendButtons += x.ToHtmlString());
                }
                if (model.prependButtons.Count() > 0)
                {
                    appendPrependContainer.AddOrMergeCssClass("input-group");
                    ((List<BootstrapButton>)model.prependButtons).ForEach(x => addOnPrependButtons += x.ToHtmlString());
                }

                appendPrependContainer.InnerHtml = addOnPrependButtons + addOnPrependString + "{0}" + addOnAppendString + addOnAppendButtons;
                combinedHtml = appendPrependContainer.ToString(TagRenderMode.Normal) + "{1}{2}";
            }

            string helpText = model.helpText != null ? model.helpText.ToHtmlString() : string.Empty;
            string validationMessage = "";
            if (model.displayValidationMessage)
            {
                string validation = html.ValidationMessage((string)model.htmlFieldName).ToHtmlString();
                validationMessage = new BootstrapHelpText(validation, model.validationMessageStyle).ToHtmlString();
            }


            TagBuilder containerDiv = new TagBuilder("div");
            containerDiv.AddOrMergeCssClass(BootstrapHelper.GetClassForInputWidth(model.width.InputWidth));
            foreach (string key in model.width.HtmlAttributes.Keys)
            {
                containerDiv.MergeAttribute(key, (string)model.width.HtmlAttributes[key]);
            }
            containerDiv.InnerHtml = MvcHtmlString.Create(string.Format(combinedHtml, input, validationMessage, helpText)).ToString();

            return containerDiv.ToString();
        }
    }
}
