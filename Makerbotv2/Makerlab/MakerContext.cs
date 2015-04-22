using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Makerlab.Models;

namespace Makerlab
{
    public class MakerContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<BookingPrintError> BookingPrintErrors { get; set; }
        public virtual DbSet<Printer> Printers { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<PrinterCommand> Printer { get; set; }

        public MakerContext() : base("MakerLabConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        } 

        
    }
}