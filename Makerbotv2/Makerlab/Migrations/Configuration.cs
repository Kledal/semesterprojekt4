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
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Makerlab.MakerContext context)
        {
            context.UserRoles.AddOrUpdate(ur => ur.RoleName,
                new UserRole { CanCreateBooking = true, RoleName = "Administrator" },
                new UserRole { CanCreateBooking = true, RoleName = "User" });
            context.Users.AddOrUpdate(u => u.Id,
                new User() { FirstName = "Test", LastName = "Person", AccessCard = 12345678, Email = "testMail@mail.dk", StudieNummer = 123456789, UserRoleId = 1 });
            context.Printers.AddOrUpdate(p => p.Id,
                  new Printer() { Name = "Gandalf", Active = true},
                  new Printer() { Name = "Frodo", Active = true },
                  new Printer() { Name = "Gollum", Active = true },
                  new Printer() { Name = "Saruon", Active = true });
            //context.Bookings.AddOrUpdate(x => x.Id,
            //    new Booking() { Id = 1, Printer = Pri}
              //  );
	}
    }
}
