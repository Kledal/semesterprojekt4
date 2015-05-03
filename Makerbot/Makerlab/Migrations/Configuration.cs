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
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Makerlab.MakerContext context)
        {
            context.UserRoles.AddOrUpdate(ur => ur.RoleName,
                new UserRole { Id = 1, CanCreateBooking = true, RoleName = "Administrator" },
                new UserRole { Id = 2, CanCreateBooking = true, RoleName = "User" });
            context.Users.AddOrUpdate(u => u.Id,
                new User() { Id = 1, FirstName = "Test", LastName = "Person", AccessCard = 12345678, Email = "testMail@mail.dk", StudieNummer = 123456789, UserRoleId = 1 });
            context.Printers.AddOrUpdate(p => p.Id,
                  new Printer() { Id = 1, Name = "Gandalf", Active = true },
                  new Printer() { Id = 2, Name = "Frodo", Active = true },
                  new Printer() { Id = 3, Name = "Gollum", Active = true },
                  new Printer() { Id = 4, Name = "Saruon", Active = true });
            context.Files.AddOrUpdate(f => f.Id,
                new File() { Id = 1, FileName = "Testfile1" },
                new File() { Id = 2, FileName = "Testfile2" });
            context.Bookings.AddOrUpdate(x => x.Id,
                new Booking() { Id = 1, PrinterId = 1, UserId = 1, FileId = 1, StartTime = new DateTime(2015, 4, 23, 8, 30, 52), EndTime = new DateTime(2015, 4, 23, 19, 00, 00) }
                );
        }
    }
}
