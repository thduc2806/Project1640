using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project1640.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Data.EF.Seeds
{
    public static class DefaultUsers
    {
        public static void SeedAsync(this ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();
            builder.Entity<User>().HasData(new User
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@mail.com",
                PasswordHash = hasher.HashPassword(null, "admin1")
            });

            builder.Entity<User>().HasData(new User
            {
                Id = 2,
                UserName = "admin2",
                Email = "admin2@mail.com",
                PasswordHash = hasher.HashPassword(null, "admin123")
            });
        }
    }
}
