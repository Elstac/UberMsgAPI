using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Classes
{
    public class LoginValidator : ILoginValidator
    {
        private UserDbContext context;
        private int tokenDuration = 10;

        public LoginValidator(UserDbContext context)
        {
            this.context = context;
        }

        public void ValidateAllLogins()
        {
            foreach (var login in context.ActiveUsers)
            {
                if (!Validate(login.TimeStamp))
                    context.ActiveUsers.Remove(login);
            }

            context.SaveChanges();
        }

        public void ValidateLogin(string token)
        {
            var query = (from active in context.ActiveUsers
                        join user in context.Users on active.Username equals user.Username
                        where active.Token == token
                        select new { active.Id, active.TimeStamp}).ToList();

            if (query.Count == 0)
                throw new NotLoggedinException("Not logged in");

            var login = query.First();

            if (Validate(login.TimeStamp))
            {
                context.ActiveUsers.Remove(context.ActiveUsers.Find(login.Id));
                context.SaveChanges();

                throw new NotLoggedinException("User session terminated");
            }
            
        }

        private bool Validate(DateTime timeStamp)
        {
            return DateTime.Now - timeStamp >= new TimeSpan(0, tokenDuration, 0);
        }
    }
}
