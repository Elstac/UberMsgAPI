using Microsoft.AspNetCore.Mvc;
using System;
using UberMsgAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UberMsgAPI.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private IUserTokenMapper mapper;
        private IMessageManager manager;
        private IEncriptionInfo encription;

        public MessageController(IUserTokenMapper validator,IMessageManager manager,IEncriptionInfo encription)
        {
            this.mapper = validator;
            this.manager = manager;
            this.encription = encription;
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
        public IActionResult Get(int id,[FromBody] ReciverRequest request)
        {
            if (id != 0)
                return BadRequest("Invalid ID");

            var sender = mapper.GetUser(request.Token).Username;

            ulong key;

            try
            {
                key = encription.GetPublicKey(sender, request.Reciver);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(new { error = e.Message });
            }

            return Ok(new { publickey = key});
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]PostMessageRequest value)
        {
            var sender = mapper.GetUser(value.Token);
            try
            {
                manager.SaveMessage(new Message { Reciver = value.Reciver, Sender = sender.Username, Content = value.Content });
            }
            catch(Exception e )
            {
                return BadRequest(new { error = e.Message});
            }

            return Ok();
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
