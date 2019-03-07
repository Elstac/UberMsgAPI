using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UberMsgAPI.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UberMsgAPI.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private UserDbContext context;
        private IHasher hasher = new Hasher();

        public RegisterController(UserDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]RegisterRequest value)
        {
            var sel = (from user in context.Passwords
                      where user.Username.ToUpper() == value.Username.ToUpper()
                      select user).ToList();

            if (sel.Count != 0)
                return BadRequest(new { error = "Account with given login already exists" });

            var salt = new byte[4];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var passHash = hasher.ComputeHash(value.Password, salt);

            context.AddAccount(value.Username, passHash.Value, salt);

            return Ok(new {answer = "Account created" });
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

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
