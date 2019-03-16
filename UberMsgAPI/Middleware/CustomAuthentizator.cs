using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UberMsgAPI.Classes;
using UberMsgAPI.Controllers;

namespace UberMsgAPI.Middleware
{
    public class CustomAuthentizator
    {
        private readonly RequestDelegate _next;

        public CustomAuthentizator(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoginValidator loginValidator)
        {
            var bodyStr = "";
            var req = context.Request;

            // Allows using several time the stream in ASP.Net Core
            req.EnableRewind();

            // Arguments: Stream, Encoding, detect encoding, buffer size 
            // AND, the most important: keep stream opened
            using (StreamReader reader
                      = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEnd();
            }

            // Rewind, so the core is not lost when it looks the body for the request
            req.Body.Position = 0;
            var ret = JsonConvert.DeserializeObject< AuthorizeRequest>(bodyStr);

            if (ret != null && ret.Token != null)
            {
                try
                {
                    loginValidator.ValidateLogin(ret.Token);
                }
                catch(NotLoggedinException e)
                {
                    await context.Response.WriteAsync(e.Message);
                    return;
                }
                await _next.Invoke(context);
            }
            else
                await _next.Invoke(context);
            
        }
    }
}
