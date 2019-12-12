using System.Web;
using System.Web.Optimization;

namespace TOS
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
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/Charts.js",
                      "~/Scripts/jquery-2.1.4.min.js",
                      "~/Scripts/jquery-3.3.1intellisense.js",
                      "~/Scripts/jquery-3.3.1.js",
                      "~/Scripts/jquery-3.3.1.min.js",
                      "~/Scripts/jquery-3.3.1.min.map",
                      "~/Scripts/jquery.basictable.min.js",
                      "~/Scripts/jquery.jqcandlestick.min.js",
                      "~/Scripts/jquery.nicescroll.js",
                      "~/Scripts/jquery.jquery-ui.js",
                      "~/Scripts/lightbox-plus-jquery.min.js",
                      "~/Scripts/monthly.js",
                      "~/Scripts/morris.js",
                      "~/Scripts/raphael-min.js",
                      "~/Scripts/scripts.js"
                      )); 

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css"
                      //"~/Content/site.css",
                      //"~/Content/bootstrap.min.css",
                      //"~/Content/manager-style.css",
                      //"~/Content/basictable.css",
                      //"~/Content/bootstrap-theme.css",
                      //"~/Content/bootstrap-theme.css.map",
                      //"~/Content/bootstrap-theme.min.css",
                      //"~/Content/boostrap-theme.min.css.map",
                      //"~/Content/bootstrap.css.map",
                      //"~/Content/bootstrap.min.css.map",
                      //"~/Content/font-awesome.css",
                      //"~/Content/icon-font.min.css",
                      //"~/Content/jqcandlestick.css",
                      //"~/Content/jquery-ui.css",
                      //"~/Content/lightbox.css",
                      //"~/Content/monthly.css",
                      //"~/Content/morris.css",
                      //"~/Content/news.css",
                      //"~/Content/order_history.css",
                      //"~/Content/order_input.css",
                      //"~/Content/order_portal.css"
                      ));
        }
    }
}
