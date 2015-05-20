using System.Linq;
using System.Security.Claims;
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
                var mail = identity.Claims.FirstOrDefault(claim => claim.Type == "eduPersonPrincipalName");

                if (mail == null) return null;
                User user = null;
                
                user = UserManager.FindByEmail(mail.Value);
                if (user != null) return user;

                var firstName = identity.Claims.FirstOrDefault(claim => claim.Type == "gn").Value;
                var lastName = identity.Claims.FirstOrDefault(claim => claim.Type == "sn").Value;

                User tempUser = new Makerlab.Models.User
                {
                    Email = mail.Value,
                    UserRole = UserRoleManager.Read().First(ur => ur.RoleName == "Default Bruger"),
                    FirstName = firstName,
                    LastName =  lastName
                };
                user = UserManager.Create(tempUser);

                return user;
            }
        }
    }
}