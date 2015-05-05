using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Makerlab.Models
{
    public enum UserRoleMethod { CanCreateBooking, CanViewDashboard }

    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool CanCreateBooking { get; set; }
        public bool CanViewDashboard { get; set; }

        // Navigation
        public virtual ICollection<User> Users { get; set; }

        public static bool Can(User u, UserRoleMethod method)
        {
            return u != null && u.UserRole.HasAccessTo(method);
        }

        private bool HasAccessTo(UserRoleMethod method)
        {
            var returnBool = false;
            switch (method)
            {
                case UserRoleMethod.CanCreateBooking:
                    returnBool = CanCreateBooking;
                    break;
                case UserRoleMethod.CanViewDashboard:
                    returnBool = CanViewDashboard;
                    break;
            }
            return returnBool;
        }

        public UserRole()
        {

        }
    }
}