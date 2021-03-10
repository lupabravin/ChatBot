using Chat.CrossCutting;
using Chat.CrossCutting.Interfaces;
using Chat.Infrastructure.Models;
using Chat.Repository;
using Chat.Repository.Interfaces;
using Chat.Services;
using Chat.Services.Interfaces;
using Chat.WebApp.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;


namespace Chat.WebApp
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
            var connectionString = GetDatabaseConnectionString();
            services.AddDbContext<ChatContext>(options =>
                options.UseSqlServer(
                    connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<AppUser>()
                .AddEntityFrameworkStores<ChatContext>();

            #region Application Services

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IProducer, Producer>();
            services.AddScoped<IConsumer, Consumer>();
            services.AddHostedService<Bot.Consumer>();

            #endregion

            #region Application Repository

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSignalR();
            services.AddControllers();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Chat}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }

        private string GetDatabaseConnectionString()
        {
            // The next lines will help us to connect to a docker database instance
            string connectionString = "";

            string database = Configuration["DBDATABASE"];
            string host = Configuration["DBHOST"];
            string password = Configuration["DBPASSWORD"];
            string port = Configuration["DBPORT"];
            string user = Configuration["DBUSER"];

            // If any of the variables is null, get connectionString from appSettings.json
            if (new List<string>() { database, host, password, port }.Any(s => s == null))
            {
                connectionString = Configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                connectionString = $"Server={host}, {port};Database={database};User Id={user};Password={password};";
            }
            return connectionString;
        }
    }
}
