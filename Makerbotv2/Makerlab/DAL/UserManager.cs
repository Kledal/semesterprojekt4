using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Makerlab.Models;

namespace Makerlab.DAL
{
    public static class UserManager
    {
        public static async Task<User> Create(User user)
        {
            using (var db = new MakerContext())
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();

                return user;
            }
        }

        public static async Task<User> Update(User user)
        {
            using (var db = new MakerContext())
            {
                db.Users.Attach(user);

                var updatedUser = db.Entry(user);
                updatedUser.State = EntityState.Modified;

                await db.SaveChangesAsync();

                return updatedUser.Entity;
            }
        }

        public static async Task<IEnumerable<User>> Read()
        {
            using (var db = new MakerContext())
            {
                var persons = await db.Users
                    .Include(u => u.UserRole)
                    .Include(u => u.Bookings)
                    .Include(u => u.BookingPrintErrors)
                    .ToListAsync();
                return persons;
            }
        }

        public static User FindByEmail(string mail)
        {
            using (var db = new MakerContext())
            {
                var user = db.Users.Include(u => u.UserRole)
                    .Include(u => u.Bookings)
                    .Include(u => u.BookingPrintErrors).SingleOrDefault(u => u.Email == mail);
                return user;
            }
        } 

        public static async Task<User> Read(int id)
        {
            using (var db = new MakerContext())
            {
                var user = await db.Users.Include(u => u.UserRole)
                    .Include(u => u.Bookings)
                    .Include(u => u.BookingPrintErrors).SingleOrDefaultAsync(u => u.Id == id);

                return user;
            }
        }

        public static async Task Delete(User user)
        {
            using (var db = new MakerContext())
            {
                db.Users.Attach(user);
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
        }
    }
}