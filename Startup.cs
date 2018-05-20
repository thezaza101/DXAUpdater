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
                string host = dbConn.Substring(dbConn.IndexOf(@"@"),dbConn.LastIndexOf(@":"));
                string user = dbConn.Substring(dbConn.IndexOf(@"/"),dbConn.IndexOf(@":"));
                string password = dbConn.Substring(dbConn.IndexOf(@"//"),dbConn.IndexOf(@"@"));
                string database = dbConn.Substring(dbConn.LastIndexOf(@"/"),dbConn.Length);
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
