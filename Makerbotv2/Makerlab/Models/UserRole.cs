using System.Collections.Generic;

namespace Makerlab.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool CanCreateBooking { get; set; }

        // Navigation
        public virtual ICollection<User> Users { get; set; }

        public UserRole()
        {
            
        }
    }
}