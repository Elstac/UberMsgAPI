using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UberMsgAPI.Classes;
using UberMsgAPI.Models;
using UberMsgAPI.Middleware;

namespace UberMsgAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<UserDbContext>(op => op.UseSqlite("Filename = Users.db"));
            
            services.AddScoped<IHasher, Hasher>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ILoginActivator , LoginActivator>();
            services.AddTransient<ILoginValidator, LoginValidator>();
            services.AddScoped<IMessageManager, MessageManager>();
            services.AddScoped<IUserTokenMapper, UserTokenMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseAuthenticator();
            app.UseMvc();
        }
    }
}
