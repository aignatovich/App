﻿using System.Web.Optimization;

namespace App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jq").Include("~/Scripts/jquery-2.1.4.min.js"));

            
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include("~/Scripts/bootstrap-datepicker.js"));
            bundles.Add(new StyleBundle("~/Content/datepicker-styles").Include("~/Content/bootstrap-datepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/multiselect").Include("~/Scripts/multiselect.min.js"));
            bundles.Add(new StyleBundle("~/Content/multiselect-styles").Include("~/Content/multiselect.css"));

            bundles.Add(new StyleBundle("~/Content/nice-select-styles").Include("~/Content/nice-select.css"));
            bundles.Add(new StyleBundle("~/Content/register-styles").Include("~/Content/register_styles.css"));

            bundles.Add(new ScriptBundle("~/bundles/register").Include(
                "~/Scripts/jquery.nice-select.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include("~/Scripts/knockout-3.3.0.js"));

            bundles.Add(new StyleBundle("~/Content/employee-jqgrid").Include(
               "~/Content/jquery-ui.css",
               "~/Content/bootstrap.min.css",
               "~/Content/ui.jqgrid-bootstrap.css",
               "~/Content/ui.jqgrid-bootstrap-ui.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include(
                "~/Scripts/grid.locale-en.js",
                "~/Scripts/jquery.jqGrid.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include("~/Plug-ins/Editor/ckeditor.js"));
            bundles.Add(new StyleBundle("~/Content/popup").Include("~/Content/popup.css"));

            bundles.Add(new ScriptBundle("~/bundles/autocomplete").Include("~/Scripts/jquery.autocomplete.js"));
            bundles.Add(new StyleBundle("~/Content/autocomplete-styles").Include("~/Content/autocomplete-styles.css"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include("~/Scripts/Chart.js"));
        }
    }
}
