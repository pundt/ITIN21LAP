using System.Web;
using System.Web.Optimization;

namespace hearthstone.web
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/content/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/content/scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/content/scripts/toastr.js"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/content/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/content/scripts/bootstrap.js",
                      "~/content/scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/content/styles/bootstrap.css",
                "~/content/styles/site.css",
                "~/Content/styles/bootstrap-theme.css",
                "~/Content/styles/toastr.css"
                      ));
        }
    }
}
