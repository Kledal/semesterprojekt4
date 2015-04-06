﻿using System.Web.Optimization;

namespace Kentor.AuthServices.StubIdp.App_Start
{
    class LessTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            response.Content = dotless.Core.Less.Parse(response.Content);
            response.ContentType = "text/css";
        }
    }
}
