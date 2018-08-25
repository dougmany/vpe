using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Toastmasters.Web.Data;
using Toastmasters.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Toastmasters.Web.Services;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.HttpOverrides;

namespace Toastmasters.Web
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
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Configuration.GetSection("AppSettings")["DP_Key_Path"]))
                .SetApplicationName(Configuration.GetSection("AppSettings")["CookieName"]);

            services.AddSingleton<IConfiguration>(_ => Configuration);
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IToastmastersRepository, ToastmastersRepository>();

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                    config.SignIn.RequireConfirmedEmail = true;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequiredLength = 8;
            })

                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<ApplicationUserManager>(); ;

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/SignIn");
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie.Name = Configuration.GetSection("AppSettings")["CookieName"];
            });

            services.AddMvc();

            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<EmailCredentials>(Configuration.GetSection("Email"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext dbContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = true
            });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //var seed = new Seed(dbContext);
        }
    }
}
