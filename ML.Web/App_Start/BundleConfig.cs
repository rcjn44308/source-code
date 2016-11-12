using System.Web.Optimization;
using ML.Core;
using ML.Core.Contracts;

namespace ML.Web
{
    public class BundleConfig : IInitializer
    {
        private readonly BundleCollection bundles;

        public BundleConfig(BundleCollection bundles)
        {
            this.bundles = bundles;
        }

        public void Initialize()
        {
            //modernizr
            this.bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            //bootrap + site styles
            this.bundles.Add(new StyleBundle("~/Content/site").Include(
                            "~/Content/bootstrap.min.css",
                            "~/Content/site.css",
                            "~/Content/PagedList.css"));
            //login style
            this.bundles.Add(new StyleBundle("~/Content/style").Include(
                "~/Content/style.css"));
//---------------------------------------------------------------------------------------------------------
            //bootstrap javascripts
            this.bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js"));
            // jQuery validate
            this.bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));
            // Jquery UI
            this.bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
            "~/Scripts/jquery-ui-{version}.js"));
            // my javascript
            this.bundles.Add(new ScriptBundle("~/bundles/myjs").Include(
                        "~/Scripts/myjs.js"));
            //Sub menu
            this.bundles.Add(new ScriptBundle("~/bundles/submenu").Include(
                       "~/Scripts/submenus.js"));
            // login JS
            this.bundles.Add(new ScriptBundle("~/bundles/mlauth").Include(
                        "~/Scripts/mljs/auth.js"));
            // Secure Internet connection
            this.bundles.Add(new ScriptBundle("~/bundles/securenet").Include(
                        "~/Scripts/internetcontrol/jquery.checknet-1.6.min.js"));
            // Modal JS Steps
            this.bundles.Add(new ScriptBundle("~/bundles/modalsteps").Include(
                        "~/Scripts/mljs/jquery-bootstrap-modal-steps.js"));
            // Steps JS
            this.bundles.Add(new ScriptBundle("~/bundles/steps").Include(
                    "~/Scripts/StepsJS/jquery.steps.js"));
            // Common JS
            this.bundles.Add(new ScriptBundle("~/bundles/common").Include(
                    "~/Scripts/mljs/common-js.js"));
            // Home JS
            this.bundles.Add(new ScriptBundle("~/bundles/homepage").Include(
                    "~/Scripts/mljs/homepage-js.js"));
            // Reports JS
            this.bundles.Add(new ScriptBundle("~/bundles/reports").Include(
                    "~/Scripts/mljs/reports-js.js"));
            // transactions JS
            this.bundles.Add(new ScriptBundle("~/bundles/trans").Include(
                    "~/Scripts/mljs/transactions-js.js"));
            // receipt JS
            this.bundles.Add(new ScriptBundle("~/bundles/receipt").Include(
                    "~/Scripts/mljs/receipt.js"));
            // KYC JS
            this.bundles.Add(new ScriptBundle("~/bundles/kyc").Include(
                    "~/Scripts/mljs/kycJS.js"));
            // Cancel JS
            this.bundles.Add(new ScriptBundle("~/bundles/cancel").Include(
                    "~/Scripts/mljs/cancellation.js"));
            // session JS
            this.bundles.Add(new ScriptBundle("~/bundles/sessionjs").Include(
                    "~/Scripts/jquerysession/jquerysession.js"));
        }
    }
}