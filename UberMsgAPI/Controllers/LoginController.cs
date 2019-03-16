using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using UberMsgAPI.Classes;
using UberMsgAPI.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UberMsgAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private UserDbContext context;
        private IHasher hasher;
        private ILoginActivator loginActivator;
        private IUserTokenMapper tokenMapper;

        public LoginController(UserDbContext context, IHasher hasher, ILoginActivator loginActivator, IUserTokenMapper tokenMapper)
        {
            this.context = context;
            this.hasher = hasher;
            this.loginActivator = loginActivator;
            this.tokenMapper = tokenMapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromBody]AuthorizeRequest request)
        {
            if (request.Token == null)
                return BadRequest("No authentication token given");

            var role = tokenMapper.GetUser(request.Token).Role;
            
            if (role != 2)
                return Unauthorized();

            return Ok( new { passwords = context.Passwords.ToList() });
        }
        
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]LoginRequest value)
        {
            var quer = from pass in context.Passwords
                         where pass.Username == value.Username
                         select new { pass.PassHash,pass.Salt};

            if(quer.Count()==0)
                return BadRequest(new { error = "Invalid user login" });

            var passwd = quer.First();

            var hash = hasher.ComputeHash(value.Password, passwd.Salt);

            try
            {
                if (hash.Equals(passwd.PassHash))
                {
                    var token = loginActivator.ActivateLogin(value.Username);
                    return Ok(new { Token = token });
                }
            }
            catch(AlreadyLoggedInException e)
            {
                return BadRequest(new { error = e.Message });
            }

            return BadRequest(new { error = "Invalid password"});
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthorizeRequest
    {
        public string Token { get; set; }
    }
}
