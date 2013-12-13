using System.Web;
using System.Web.Optimization;

namespace BeerTech
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/mainscripts").Include(
                "~/Scripts/jquery-2.0.3.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/angular.min.js",
                "~/Scripts/ngapps/user.js"));

            bundles.Add(new StyleBundle("~/bundles/mainstyles").Include(
                "~/Content/CSS/bootstrap.min.css",
                "~/Content/CSS/bootstrap-theme.min.css",
                "~/Content/CSS/jumbotron.css"));
        }
    }
}