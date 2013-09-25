﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TwitterBootstrapMVC.Infrastructure;

namespace TwitterBootstrapMVC.Controls
{
    public class AlertBuilder<TModel> : BuilderBase<TModel, Alert>
    {
        internal AlertBuilder(HtmlHelper<TModel> htmlHelper, Alert alert)
            : base(htmlHelper, alert)
        {
            if (this.element.closeable)
                base.textWriter.Write(@"<button type=""button"" class=""close"" data-dismiss=""alert"">&times;</button>");
        }
    }
}
