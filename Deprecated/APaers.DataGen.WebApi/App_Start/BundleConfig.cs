using System.Web.Optimization;
using APaers.DataGen.WebApi.Helpers;

namespace APaers.DataGen.WebApi
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            foreach (string theme in ThemeHelper.Themes)
            {
                string stylePath = $"~/Content/Themes/{theme}/bootstrap.css";

                bundles.Add(new StyleBundle(ThemeHelper.Bundle(theme)).Include(
                      stylePath,
                      "~/Content/site.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/angular-block-ui.min.css"));
            }

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
               "~/Scripts/angular.js",
               "~/Scripts/angular-route.js",
               "~/Scripts/angular-block-ui.js",
               "~/Scripts/angular-ui/ui-bootstrap.min.js",
               "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/shared").Include(
               "~/Views/Shared/dataGenBootstrap.js",
               "~/Views/Shared/app.consts.js",
               "~/Views/Shared/dataGenRouting.js",
               "~/Views/Shared/masterController.js",
                "~/Views/Shared/ajaxService.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                "~/Views/Home/indexController.js"
            ));
        }
    }
}
