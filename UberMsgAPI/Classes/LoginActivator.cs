using System;
using System.Linq;

namespace UberMsgAPI.Classes
{
    public class LoginActivator : ILoginActivator
    {
        private ITokenGenerator tokenGenerator;
        private UserDbContext context;

        public LoginActivator(ITokenGenerator tokenGenerator, UserDbContext context)
        {
            this.tokenGenerator = tokenGenerator;
            this.context = context;
        }
        
        public string ActivateLogin(string username)
        {
            var us = (from au in context.ActiveUsers
                      where au.Username == username
                      select au).ToList().Count;

            if (us != 0)
                throw new AlreadyLoggedInException("Already logged in");

            var token = tokenGenerator.GetToken(10);
            context.ActiveUsers.Add(new ActiveUser { TimeStamp = DateTime.Now, Token = token, Username = username });
            context.SaveChanges();

            return token;
        }
    }
}
