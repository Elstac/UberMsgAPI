﻿using Microsoft.EntityFrameworkCore;
namespace UberMsgAPI
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<ActiveUser> ActiveUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Connection> Connections { get; set; }

        public void AddAccount(string username,byte[] passHash,byte[] salt)
        {
            var toAdd = new Password { Username = username, PassHash = passHash, Salt = salt };

            Passwords.Add(toAdd);

            SaveChanges();
        }
    }
}
