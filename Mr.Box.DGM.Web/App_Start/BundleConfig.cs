using System.Web;
using System.Web.Optimization;

namespace Mr.Box.DGM.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //jquery
            bundles.Add(new ScriptBundle("~/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

             
            
            bundles.Add(new ScriptBundle("~/AdminExjs").Include(
                "~/Scripts/js/AdminEx/jquery-ui-1.9.2.custom.min.js",
                "~/Scripts/js/AdminEx/jquery-migrate-1.2.1.min.js",
                "~/Scripts/js/AdminEx/jquery.nicescroll.js",
                "~/Scripts/modernizr-2.6.2.js",
                "~/Scripts/js/AdminEx/scripts.js"
                ));

            bundles.Add(new StyleBundle("~/AdminExcss").Include(
                "~/Content/AdminEx/style.css",
                "~/Content/AdminEx/style-responsive.css"
                ));


            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bootstrap
            bundles.Add(new ScriptBundle("~/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/bootstrap/bootbox.min.js",
                      "~/Scripts/layer/layer.js"
                     //,"~/Scripts/respond.js"
                     ));

            //datetimepicker
            bundles.Add(new ScriptBundle("~/datetimepicker").Include(
                    "~/Scripts/bootstrap/datetimepicker/js/bootstrap-datetimepicker.js",
                    "~/Scripts/bootstrap/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"));

            //datetimepicker2
            bundles.Add(new ScriptBundle("~/datetimepicker2").Include(
                "~/Scripts/bootstrap/datetimepicker/date/moment.js",
                "~/Scripts/bootstrap/datetimepicker/date/daterangepicker.js"));



            //bootstraptable
            bundles.Add(new ScriptBundle("~/bootstraptable").Include(
                "~/Scripts/bootstrap/table/bootstrap-table.js").Include(
                "~/Scripts/bootstrap/table/locale/bootstrap-table-zh-CN.js",
                "~/Scripts/bootstrap/table/extensions/export/bootstrap-table-export.js",
                "~/Scripts/bootstrap/table/extensions/export/tableExport.js",
                "~/Scripts/bootstrap/table/extensions/editable/bootstrap-table-editable.js",
                "~/Scripts/bootstrap/table/extensions/editable/bootstrap-editable.js"
                ));

            //css
            bundles.Add(new StyleBundle("~/css").Include(
                "~/Content/bootstrap/css/bootstrap.css",
                "~/Scripts/bootstrap/table/extensions/editable/bootstrap-editable.css",
                "~/Content/css/animate.css",
                "~/Content/css/common.css",
                "~/Content/font-awesome-4.7.0/css/font-awesome.min.css"
                ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
