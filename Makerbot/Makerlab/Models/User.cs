using System.Collections.Generic;
using System.ComponentModel;

namespace Makerlab.Models
{
    public class User
    {
        public int Id { get; set; }
        [DisplayName("Fornavn")]
        public string FirstName { get; set; }
        [DisplayName("Efternavn")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public int StudieNummer { get; set; }
        [DisplayName("Adgangskortnr.")]
        public int AccessCard { get; set; }

        
        public int UserRoleId { get; set; }

        // Navigation
        [DisplayName("Brugerrolle")]
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

        public User()
        {
            
        }
    }
}