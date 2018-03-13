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
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/Bootstrap plugins/Select/bootstrap-select.min.js",
                      "~/Scripts/vue.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css",
                      "~/Content/Bootstrap plugins/Select/bootstrap-select.min.css",
                      "~/Content/Main.css"));

            bundles.Add(new ScriptBundle("~/bundles/AdminSectionScripts").Include(
                "~/Scripts/Javascript/AdminSection/AdminSection.js",
                "~/Scripts/Bootstrap plugins/Fileinput/fileinput.js",
                "~/Scripts/Bootstrap plugins/Fileinput/theme.min.js",
                "~/Scripts/Bootstrap plugins/Fileinput/sortable.min.js",
                "~/Scripts/Bootstrap plugins/Fileinput/nl.js"));

            bundles.Add(new StyleBundle("~/bundles/AdminSectionCss").Include(
                "~/Content/AdminSection/Admin.css",
                "~/Content/Bootstrap plugins/Fileinput/fileinput.min.css",
                "~/Content/Bootstrap plugins/Fileinput/theme.min.css"));
        }
    }
}
