using System;
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
            try
            {
                dbConn = Environment.GetEnvironmentVariable("DATABASE_URL").Trim();
                System.Console.WriteLine(dbConn);
            }
            catch
            {
                System.Console.WriteLine("Error getting DATABASE_URL");
            }
            if (string.IsNullOrEmpty(dbConn))
            {
            services.AddDbContext<UpdatedDataContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                string host = dbConn.Substring(dbConn.IndexOf(@"@")+1,dbConn.LastIndexOf(@":")-dbConn.IndexOf(@"@")-1);
                string user = dbConn.Substring(dbConn.IndexOf(@"//")+2,dbConn.IndexOf(@":",dbConn.IndexOf(@"//"))-dbConn.IndexOf(@"//")-2);
                string password = dbConn.Substring(dbConn.IndexOf(user.Substring(user.Length-3))+4,dbConn.IndexOf(@"@")-dbConn.IndexOf(user.Substring(user.Length-3))-4);
                string database = dbConn.Substring(dbConn.LastIndexOf(@"/")+1,dbConn.Length-dbConn.LastIndexOf(@"/")-1);
                string databaseConnectionString = "Host="+host+";Database="+database+";Username="+user+";Password="+password+";";
                System.Console.WriteLine(databaseConnectionString);
                services.AddDbContext<UpdatedDataContext>(options =>
                options.UseNpgsql(databaseConnectionString));
            }
                
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
