using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UberMsgAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UberMsgAPI.Controllers
{
    [Route("api/[controller]")]
    public class KeyExchangeController : Controller
    {
        private IKeyManager keyManager;
        private IUserTokenMapper mapper;

        public KeyExchangeController(IKeyManager keyManager, IUserTokenMapper mapper)
        {
            this.keyManager = keyManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Connection> Get([FromBody] AuthorizeRequest request)
        {
            var user = mapper.GetUser(request.Token);

            return keyManager.GetPendingConnections(user.Username);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id != 0)
                return BadRequest("Invalid ID");

            var gen = keyManager.GetGenerator();
            return Ok(new { g = gen.Item1, n = gen.Item2 });
        }
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]ConnectionRequest request)
        {
            try
            {
                keyManager.EstablishConnection(request.Reciver, mapper.GetUser(request.Token).Username, request.PublicKey);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok("Connection established/pending");
        }
    }

    public class ConnectionRequest:ReciverRequest
    {
        public ulong PublicKey { get; set; }
    }
}
