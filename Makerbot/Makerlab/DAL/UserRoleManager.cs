using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Makerlab.Models;

namespace Makerlab.DAL
{
    public class UserRoleManager
    {
        public static async Task<UserRole> Create(UserRole userRole)
        {
            using (var db = new MakerContext())
            {
                db.UserRoles.Add(userRole);
                await db.SaveChangesAsync();

                return userRole;
            }
        }

        public static async Task<UserRole> Update(UserRole userRole)
        {
            using (var db = new MakerContext())
            {
                db.UserRoles.Attach(userRole);

                var updatedUser = db.Entry(userRole);
                updatedUser.State = EntityState.Modified;

                await db.SaveChangesAsync();

                return updatedUser.Entity;
            }
        }

        public static List<UserRole> Read()
        {
            using (var db = new MakerContext())
            {
                var userRoles = db.UserRoles
                    .Include(u => u.Users);
                return userRoles.ToList();
            }
        }

        public static UserRole Read(int id)
        {
            using (var db = new MakerContext())
            {
                var userRole = db.UserRoles.Include(u => u.Users).SingleOrDefault(u => u.Id == id);

                return userRole;
            }
        }

        public static async Task Delete(UserRole userRole)
        {
            using (var db = new MakerContext())
            {
                db.UserRoles.Attach(userRole);
                db.UserRoles.Remove(userRole);
                await db.SaveChangesAsync();
            }
        }
    }
}