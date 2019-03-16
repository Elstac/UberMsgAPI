using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UberMsgAPI.Classes;
using UberMsgAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UberMsgAPI.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private IUserTokenMapper mapper;
        private IMessageManager manager;

        public MessageController(IUserTokenMapper validator,IMessageManager manager)
        {
            this.mapper = validator;
            this.manager = manager;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromBody] GetMessageRequest request)
        {
            var user = mapper.GetUser(request.Token);

            var msgs = manager.GetMessages(new string[] { user.Username, request.Reciver });

            return Ok(new { messages = msgs});
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]PostMessageRequest value)
        {
            
        }
    }

    public class ReciverRequest : AuthorizeRequest
    {
        public string Reciver { get; set; }
    }
    
    public class GetMessageRequest: ReciverRequest
    {
        public int Count { get; set; }
    }
    
    public class PostMessageRequest:ReciverRequest
    {
        public string Content { get; set; }
    }
}
