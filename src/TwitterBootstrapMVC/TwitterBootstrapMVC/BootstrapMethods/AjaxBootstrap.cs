using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TwitterBootstrapMVC.Controls;

namespace TwitterBootstrapMVC.BootstrapMethods
{
    public partial class AjaxBootstrap<TModel>
    {
        public AjaxHelper<TModel> Ajax;

        public AjaxBootstrap(AjaxHelper<TModel> _ajax)
        {
            this.Ajax = _ajax;
        }

        //public AjaxBootstrapContainer Container()
        //{
        //    return new AjaxBootstrapContainer(Ajax);
        //}

        //public BootstrapControlGroup<TModel> ControlGroup()
        //{
        //    return new AjaxBootstrapControlGroup<TModel>(Ajax);
        //}

        //public BootstrapHelpText HelpText(string text, HelpTextStyle style)
        //{
        //    return new BootstrapHelpText(text, style);
        //}

        //public BootstrapDropDownMenu DropDownMenu()
        //{
        //    return new BootstrapDropDownMenu();
        //}

        //public BootstrapIcon Icon(Icons icon)
        //{
        //    return new BootstrapIcon(icon);
        //}

        //public BootstrapIcon Icon(string customCssClass)
        //{
        //    return new BootstrapIcon(customCssClass);
        //}
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return null; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return base.Equals(obj); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return base.GetHashCode(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); }
    }
}
