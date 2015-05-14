using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Makerlab.DAL;
using Makerlab.Models;
using RedirectToRouteResult = System.Web.Http.Results.RedirectToRouteResult;

namespace Makerlab.Extensions
{
    public class AuthAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;

        public AuthAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.User;
            if (!user.Identity.IsAuthenticated) return false;

            var identity = (ClaimsIdentity)user.Identity;
            var mail = identity.Claims.FirstOrDefault(claim => claim.Type == "eduPersonPrincipalName");
            if (mail == null) return false;

            var u = UserManager.FindByEmail(mail.Value);
            if (u == null) return false;

            return allowedroles.Contains(u.UserRole.RoleName);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpStatusCodeResult(403);
        }
    }
}