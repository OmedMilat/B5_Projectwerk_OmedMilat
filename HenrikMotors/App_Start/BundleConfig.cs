using System.Web;
using System.Web.Optimization;

namespace HenrikMotors
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-combobox.js",
                      "~/Scripts/bootstrap-select.min.js",
                      "~/Scripts/vue.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/Content/bootstrap-combobox.css",
                      "~/Content/bootstrap-select.min.css",
                      "~/Content/Main.css"));

            bundles.Add(new ScriptBundle("~/bundles/AdminSectionScripts").Include(
                "~/Scripts/Javascript/AdminSection/AdminSection.js",
                "~/Scripts/fileinput.js",
                "~/Scripts/sortable.min.js"));

            bundles.Add(new StyleBundle("~/bundles/AdminSectionCss").Include(
                "~/Content/AdminSection/Admin.css",
                "~/Content/fileinput.min.css"));
        }
    }
}
