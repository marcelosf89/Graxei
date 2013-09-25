﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TwitterBootstrapMVC.Infrastructure;
using TwitterBootstrapMVC.TypeExtensions;

namespace TwitterBootstrapMVC.Controls
{
    public class DropDownBuilder<TModel> : BuilderBase<TModel, DropDown>
    {
        private string wrapperTag;

        internal DropDownBuilder(HtmlHelper<TModel> htmlHelper, DropDown dropDown, string wrapperTag = null, object wrapperTagHtmlAttributes = null)
            : base(htmlHelper, dropDown)
        {
            this.wrapperTag = wrapperTag;
            string beginWrapperTag = string.Empty;
            if (!string.IsNullOrEmpty(this.wrapperTag))
            {
                TagBuilder wrapperTagBuilder = new TagBuilder(this.wrapperTag);
                wrapperTagBuilder.MergeAttributes(wrapperTagHtmlAttributes.ObjectToHtmlAttributesDictionary());
                beginWrapperTag = wrapperTagBuilder.ToString(TagRenderMode.StartTag);
            }
            
            TagBuilder trigger = new TagBuilder("a");
            trigger.AddOrMergeAttribute("data-toggle", "dropdown");
            trigger.AddOrMergeAttribute("href", "#");
            if (this.wrapperTag != "li") trigger.AddCssClass("btn dropdown-toggle");
            trigger.InnerHtml = base.element.actionText + (this.wrapperTag != "li" ? @" <span class=""caret""></span>" : string.Empty);

            if (!string.IsNullOrEmpty(this.wrapperTag)) base.textWriter.Write(beginWrapperTag);
            base.textWriter.Write(trigger.ToString(TagRenderMode.Normal));
            base.textWriter.Write(@"<ul class=""dropdown-menu"">");
        }

        public BootstrapLink Link(string linkText, string url)
        {
            return new BootstrapLink(base.htmlHelper, linkText, url).WrapInto("li");
        }

        public BootstrapActionLink ActionLink(string linkText, ActionResult result)
        {
            return new BootstrapActionLink(htmlHelper, linkText, result).WrapInto("li");
        }

        public BootstrapActionLink ActionLink(string linkText, string actionName)
        {
            return new BootstrapActionLink(htmlHelper, linkText, actionName).WrapInto("li");
        }

        public BootstrapActionLink ActionLink(string linkText, string actionName, string controllerName)
        {
            return new BootstrapActionLink(htmlHelper, linkText, actionName, controllerName).WrapInto("li");
        }

        public void Divider()
        {
            base.textWriter.Write(@"<li class=""divider""></li>");
        }

        public DropDownBuilder<TModel> BeginSubMenu(DropDown dropDown)
        {
            return new DropDownBuilder<TModel>(base.htmlHelper, dropDown, "li", new { @class = "" });
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void Dispose()
        {
            base.textWriter.Write("</ul>");
            if (!string.IsNullOrEmpty(this.wrapperTag)) base.textWriter.Write(string.Format("</{0}>", this.wrapperTag));
            base.Dispose();
        }
    }
}
