using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Models
{
    public class UserTokenMapper : IUserTokenMapper
    {
        private UserDbContext context;

        public UserTokenMapper(UserDbContext context)
        {
            this.context = context;
        }

        public User GetUser(string token)
        {
            var query = (from active in context.ActiveUsers
                         join user in context.Users on active.Username equals user.Username
                         where active.Token == token
                         select new User {Id= active.Id,Role= user.Role,Username = user.Username }).ToList();

            return query.First();
        }
    }
}
