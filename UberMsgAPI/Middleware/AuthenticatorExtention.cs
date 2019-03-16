using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Middleware
{
    public static class AuthenticatorExtention
    {
        public static IApplicationBuilder UseAuthenticator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthentizator>();
        }
    }
}
