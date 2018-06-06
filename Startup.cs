using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using DXAUpdater.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using DXAUpdater.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DXAUpdater
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
            string dbConn = "";
            dbConn = "Server=tcp:dxaupdater.database.windows.net,1433;Initial Catalog=dxa-updater;Persist Security Info=False;User ID=testdbxx;Password=$testxx123Xdb;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            try
            {
                dbConn = Environment.GetEnvironmentVariable("AzureDB").Trim();
                System.Console.WriteLine(dbConn);
            }
            catch
            {
                System.Console.WriteLine("Error getting AzureDB");
            }
            if (string.IsNullOrEmpty(dbConn))
            {
                services.AddDbContext<UpdatedDataContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                services.AddDbContext<UpdatedDataContext>(options =>
                   options.UseSqlServer(dbConn));
            }
            /* PLACEHOLDER
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("dd%88*377f6d&f£$$£$FdddFF33fssDG^!3"))
                };
            }); 
            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(  options =>  {  
                    options.SecurityTokenValidators.Clear();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityTokenKey"])),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"])),
                        //ValidAudience = "localhost",
                        //ValidIssuer = "localhost",
                        ValidateLifetime = false,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
            });
            */
            

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseAuthentication();
            app.UseMvc();


        }

    }

}
