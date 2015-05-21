using System;
using System.Data.Entity.Migrations;
using Makerlab.Models;

namespace Makerlab.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MakerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MakerContext context)
        {
            context.UserRoles.AddOrUpdate(ur => ur.RoleName,
                new UserRole { Id = 1, CanCreateBooking = true, CanViewDashboard = true, RoleName = "Administrator" },
                new UserRole { Id = 2, CanCreateBooking = true, CanViewDashboard = false, RoleName = "Godkendt Bruger" },
                new UserRole { Id = 3, CanCreateBooking = false, CanViewDashboard = false, RoleName = "Default Bruger" });
            context.Users.AddOrUpdate(u => u.Id,
                new User()
                {
                    Id = 1, 
                    FirstName = "Test", 
                    LastName = "Person", 
                    AccessCard = 12345678, 
                    Email = "jj@testidp.wayf.dk", 
                    StudieNummer = 123456789, 
                    UserRoleId = 1
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Ikke",
                    LastName = "Godkendt",
                    AccessCard = 123,
                    Email = "test@test.dk",
                    StudieNummer = 321,
                    UserRoleId = 3
                });
            context.Printers.AddOrUpdate(p => p.Id,
                  new Printer() { Id = 1, Name = "Gandalf", IsBookable = true, LastSeen = DateTime.Now, UuId = "55330343434351D072C1" },
                  new Printer() { Id = 2, Name = "Frodo", IsBookable = true, LastSeen = DateTime.Now },
                  new Printer() { Id = 3, Name = "Gollum", IsBookable = true, LastSeen = DateTime.Now, UuId = "55330343534351103222" },
                  new Printer() { Id = 4, Name = "Saruon", IsBookable = true, LastSeen = DateTime.Now });
            context.Bookings.AddOrUpdate(x => x.Id,
                new Booking()
                {
                    Id = 1, 
                    PrinterId = 1, 
                    UserId = 1, 
                    StartTime = new DateTime(2015, 5, 14, 8, 30, 00), 
                    EndTime = new DateTime(2015, 5, 14, 19, 00, 00)
                });
            context.Files.AddOrUpdate(x => x.Id,
                new File
                {
                    Id = 1,
                    FileName = "test.gcode",
                    FilePath = "test.gcode",
                    NumberOfLines = 1
                });
        }
    }
}
