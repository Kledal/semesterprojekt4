using System.EnterpriseServices;
using System.Web.WebPages;
using Makerlab.Models;

namespace Makerlab.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Makerlab.MakerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Makerlab.MakerContext context)
        {
            context.UserRoles.AddOrUpdate(ur => ur.RoleName,
                new UserRole { CanCreateBooking = true, RoleName = "Administrator" },
                new UserRole { CanCreateBooking = true, RoleName = "User" }); 
            context.Users.AddOrUpdate(u => u.Id,
                new User() { FirstName = "Test", LastName = "Person", AccessCard = 12345678, Email = "testMail@mail.dk", StudieNummer = 123456789, UserRoleId = 1});
            context.Printers.AddOrUpdate(p => p.Id,
                  new Printer() { Name = "Gandalf", },
                  new Printer() { Name = "Frodo", },
                  new Printer() { Name = "Gollum", },
                  new Printer() { Name = "Saruon", });
            //context.Bookings.AddOrUpdate(x => x.Id,
                //new Booking() { Id = 1, File = "Testfile1", Printer = Printer.1}
               // );
        }
    }
}
