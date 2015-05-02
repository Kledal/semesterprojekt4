using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Makerlab.DAL;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    public class ApplicationController : Controller
    {
        public User CurrentUser
        {
            get
            {
                if (!User.Identity.IsAuthenticated) return null;

                var identity = (ClaimsIdentity)User.Identity;
                var mail = identity.Claims.FirstOrDefault(claim => claim.Type == "mail");
                return mail != null ? UserManager.FindByEmail(mail.Value) : null;
            }
        }
    }
}