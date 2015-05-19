﻿using System.Web.Optimization;
using Kentor.AuthServices.StubIdp.App_Start;

namespace Kentor.AuthServices.StubIdp
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/Content/css")
                {
                    Transforms = { new LessTransform() }
                }
                .Include(
                "~/Content/normalize.css",
                "~/Content/site.less"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));
        }
    }
}