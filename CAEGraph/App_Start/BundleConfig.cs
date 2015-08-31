using System.Web;
using System.Web.Optimization;

namespace CAEGraph
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/bower_components/angular/angular.js",
                "~/bower_components/angular-bootstrap/ui-bootstrap.js",
                "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.js",
                "~/Scripts/app.js",
                "~/Scripts/services.js",
                "~/Scripts/controllers.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                "~/bower_components/d3/d3.js",
                "~/bower_components/c3/c3.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/bower_components/moment/moment.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
                "~/bower_components/bootstrap/dist/css/bootstrap.css"));

            bundles.Add(new StyleBundle("~/bundles/chart.css").Include(
                "~/bower_components/c3/c3.css"));

            
        }
    }
}
