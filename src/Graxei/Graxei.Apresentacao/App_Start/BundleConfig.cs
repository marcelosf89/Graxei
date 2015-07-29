using System;
using System.Web;
using System.Web.Optimization;

namespace Graxei.Apresentacao
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/sc").Include(
                        "~/Scripts/tooltip.js",
                        "~/Scripts/graxei.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.11.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalajax").Include(
                    "~/Scripts/jquery-2.1.4.min.js",
                    "~/Scripts/jquery.validate.min.js",
                    "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymaskmoney").Include(
                "~/Scripts/jquery.maskMoney.js"));

            bundles.Add(new ScriptBundle("~/bundles/typeahead").Include(
                "~/Scripts/typeahead.jquery.js", "~/Scripts/bloodhound.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-2.1.4.min.js",
            "~/Scripts/jquery-migrate-1.2.1.min.js"));

           bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

           bundles.Add(new ScriptBundle("~/Administrativo/Enderecos/Formulario/js")
                  .Include("~/Scripts/Graxei/Administrativo/Enderecos/formularioEndereco.js"));

           bundles.Add(new ScriptBundle("~/Administrativo/Produtos/Pesquisar/js")
                   .Include("~/Scripts/Graxei/Administrativo/Produtos/pesquisar.js")
                   .Include("~/Scripts/Graxei/Administrativo/Produtos/pesquisar.eventos.js"));

            bundles.Add(new StyleBundle("~/css/themes").Include(
                "~/Content/graxei.min.css",
                "~/Content/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/css/themes/base/css").Include(
                        "~/Content/themes/base/minified/jquery.ui.core.min.css",
                        "~/Content/themes/base/minified/jquery.ui.resizable.min.css",
                        "~/Content/themes/base/minified/jquery.ui.selectable.min.css",
                        "~/Content/themes/base/minified/jquery.ui.accordion.min.css",
                        "~/Content/themes/base/minified/jquery.ui.autocomplete.min.css",
                        "~/Content/themes/base/minified/jquery.ui.button.min.css",
                        "~/Content/themes/base/minified/jquery.ui.dialog.min.css",
                        "~/Content/themes/base/minified/jquery.ui.slider.min.css",
                        "~/Content/themes/base/minified/jquery.ui.tabs.min.css",
                        "~/Content/themes/base/minified/jquery.ui.datepicker.min.css",
                        "~/Content/themes/base/minified/jquery.ui.progressbar.min.css",
                        "~/Content/themes/base/minified/jquery.ui.theme.min.css",
                        "~/Content/themes/base/minified/jquery-ui.min.css"
                        ));

            bundles.Add(new StyleBundle("~/css/fileinput").Include(
            "~/Scripts/fileinput.js"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}