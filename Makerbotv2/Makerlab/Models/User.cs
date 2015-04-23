using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Makerlab.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int StudieNummer { get; set; }
        public int AccessCard { get; set; }

        [ForeignKey("UserRole")]
        public virtual int UserRoleId { get; set; }

        // Navigation
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<BookingPrintError> BookingPrintErrors { get; set; }

        public User()
        {
            
        }
    }
}