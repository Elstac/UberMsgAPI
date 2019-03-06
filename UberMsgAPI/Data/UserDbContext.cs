using Microsoft.EntityFrameworkCore;
namespace UberMsgAPI
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
    }
}
