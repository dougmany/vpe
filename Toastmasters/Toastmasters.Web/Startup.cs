using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Toastmasters.Web.Data;
using Toastmasters.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Toastmasters.Web.Services;

namespace Toastmasters.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Startup>();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_ => Configuration);
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IToastmastersRepository, ToastmastersRepository>();

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<ApplicationUserManager>(); ;

            services.AddMvc();

            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<EmailCredentials>(Configuration.GetSection("Email"));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext dbContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Account/SignIn"),
                ExpireTimeSpan = TimeSpan.FromDays(30),
            });

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            dbContext.Database.Migrate();

            var seed = new Seed(dbContext);
        }
    }
}
