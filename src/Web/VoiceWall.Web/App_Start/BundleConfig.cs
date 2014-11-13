using System.Web;
using System.Web.Optimization;

namespace VoiceWall.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScriptBundles(bundles);
            RegisterStylesBundles(bundles);

            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo/kendo.all.min.js",
                        "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                // using the kendo jquery version
                        "~/Scripts/kendo/jquery.min.js",
                        "~/Scripts/jquery.transit.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/sideMenu").Include(
                     "~/Scripts/app/sideMenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/loadingImageHider").Include(
                     "~/Scripts/app/loadingImageHider.js"));

            bundles.Add(new ScriptBundle("~/bundles/wallItems").Include(
                    "~/Scripts/jquery.easing.js",
                    "~/Scripts/jquery.fancybox.js",
                    "~/Scripts/app/wallItemHolder.js",
                    "~/Scripts/app/contentSender.js",
                    "~/Scripts/app/videoRecording/RecordRTC.js",
                    "~/Scripts/app/videoRecording/videoMerging.js",
                    "~/Scripts/app/videoRecording/videoRecording.js",
                    "~/Scripts/app/soundRecording/audiodisplay.js",
                     "~/Scripts/app/soundRecording/recorder.js",
                     "~/Scripts/app/soundRecording/main.js",
                     "~/Scripts/app/pictureRecording/main.js"));
        }

        private static void RegisterStylesBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                     "~/Content/kendo/kendo.common.min.css",
                     "~/Content/kendo/kendo.common-bootstrap.min.css",
                     "~/Content/kendo/kendo.default.min.css"));

            bundles.Add(new StyleBundle("~/Content/wallItems").Include(
                     "~/Content/soundRecording.css",
                     "~/Content/videoRecording.css",
                      "~/Content/wallItemContent.css"));

            bundles.Add(new StyleBundle("~/Content/libs").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/toastr.css",
                      "~/Content/jquery.fancybox.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/site.css"));
        }
    }
}
