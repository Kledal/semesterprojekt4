using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Makerlab.Models;

namespace Makerlab.DAL
{
    public static class UserManager
    {
        public static User Create(User user)
        {
            using (var db = new MakerContext())
            {
                db.Users.Add(user);
                db.SaveChanges();

                return user;
            }
        }

        public static User Update(User user)
        {
            using (var db = new MakerContext())
            {
                db.Users.Attach(user);

                var updatedUser = db.Entry(user);
                updatedUser.State = EntityState.Modified;

                db.SaveChanges();

                return updatedUser.Entity;
            }
        }

        public static IEnumerable<User> Read()
        {
            using (var db = new MakerContext())
            {
                var persons = db.Users
                    .Include(u => u.UserRole)
                    .Include(u => u.Bookings)
                    .ToList();
                return persons;
            }
        }

        public static User FindByEmail(string mail)
        {
            using (var db = new MakerContext())
            {
                var user = db.Users.Include(u => u.UserRole)
                    .Include(u => u.Bookings).SingleOrDefault(u => u.Email == mail);
                return user;
            }
        }

        public static User Read(int id)
        {
            using (var db = new MakerContext())
            {
                var user = db.Users.Include(u => u.UserRole)
                    .Include(u => u.Bookings).SingleOrDefault(u => u.Id == id);

                return user;
            }
        }

        public static void Delete(User user)
        {
            using (var db = new MakerContext())
            {
                db.Users.Attach(user);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
    }
}