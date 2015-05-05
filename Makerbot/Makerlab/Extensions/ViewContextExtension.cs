using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Makerlab.Controllers;
using Makerlab.Models;

namespace Makerlab.Extensions
{
    public static class ViewContextExtension
    {
        public static bool Can(this ViewContext view, UserRoleMethod method)
        {
            var user = CurrentUser(view);
            return UserRole.Can(user, method);
        }

        public static User CurrentUser(this ViewContext view)
        {
            var baseController = (ApplicationController) view.Controller;
            return baseController.CurrentUser;
        }

        public static ApplicationController BaseController(this ViewContext view)
        {
            var baseController = (ApplicationController)view.Controller;
            return baseController;
        }
    }
}