using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Graxei.Apresentacao.MVC4Unity.App_Start.BootstrapBundleConfig), "RegisterBundles")]

namespace Graxei.Apresentacao.MVC4Unity.App_Start
{
	public class BootstrapBundleConfig
	{
		public static void RegisterBundles()
		{
			// Add @Styles.Render("~/Content/bootstrap/base") in the <head/> of your _Layout.cshtml view
			// For Bootstrap theme add @Styles.Render("~/Content/bootstrap/theme") in the <head/> of your _Layout.cshtml view
			// Add @Scripts.Render("~/bundles/bootstrap") after jQuery in your _Layout.cshtml view
			// When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.min.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/modal").Include("~/Scripts/modal.js"));
			BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap/base").Include("~/Content/bootstrap/bootstrap.min.css"));
			BundleTable.Bundles.Add(new StyleBundle("~/css/bootstrap/theme").Include("~/Content/bootstrap/bootstrap-theme.min.css"));
		}
	}
}
