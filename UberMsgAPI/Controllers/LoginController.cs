using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UberMsgAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private UserDbContext context;

        public LoginController(UserDbContext context)
        {
            this.context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]LoginRequest value)
        {
            var quer = from pass in context.Passwords
                         where pass.Username == value.Username
                         select pass.PassHash;

            if(quer.Count()==0)
                return BadRequest(new { error = "Invalid user login" });

            var passwd = quer.First();

            if (passwd == value.Password)
                return Ok(new { ans = "Loggedin correctly" });
            else
                return BadRequest(new { error = "Invalid password" });

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
}
