using System.Web;
using System.Web.Optimization;

namespace VoiceWall.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.transit.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/sideMenu").Include(
                     "~/Scripts/app/sideMenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/commentContent").Include(
                    "~/Scripts/app/videoRecording/RecordRTC.js",
                    "~/Scripts/app/videoRecording/videoMerging.js",
                    "~/Scripts/app/videoRecording/videoRecording.js",
                    "~/Scripts/app/soundRecording/audiodisplay.js",
                     "~/Scripts/app/soundRecording/recorder.js",
                     "~/Scripts/app/soundRecording/main.js",
                     "~/Scripts/app/pictureRecording/main.js"));

            bundles.Add(new StyleBundle("~/Content/commentContent").Include(
                     "~/Content/soundRecording.css",
                     "~/Content/videoRecording.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
