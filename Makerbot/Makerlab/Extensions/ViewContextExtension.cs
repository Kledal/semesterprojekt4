using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Makerlab.Controllers;

namespace Makerlab.Extensions
{
    public static class ViewContextExtension
    {
        public static ApplicationController BaseController(this ViewContext view)
        {
            var baseController = (ApplicationController)view.Controller;
            return baseController;
        }
    }
}