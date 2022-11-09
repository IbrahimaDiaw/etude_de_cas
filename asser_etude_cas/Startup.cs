using asser_etude_cas.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas
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
            services.AddControllersWithViews();
            services.AddDbContext<ASERDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AserDBConnectionString")));

            services.AddDefaultIdentity<IdentityUser>
                (options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 10;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
            .AddEntityFrameworkStores<ASERDbContext>();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private async Task CreateUsersAndRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "Agent", "Guest" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1 
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("admin@admin.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "Ibrahima",
                    Email = "admin@admin.com",
                };
                await UserManager.CreateAsync(user, "123456!");
            }
            await UserManager.AddToRoleAsync(user, "Admin");

            IdentityUser user1 = await UserManager.FindByEmailAsync("agent@gmail.com");

            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = "Agent",
                    Email = "agent@gmail.com",
                };
                await UserManager.CreateAsync(user1, "agent123");
            }
            await UserManager.AddToRoleAsync(user1, "Agent");

            IdentityUser user3 = await UserManager.FindByEmailAsync("guest@gmail.com");

            if (user3 == null)
            {
                user3 = new IdentityUser()
                {
                    UserName = "Guest",
                    Email = "guest@gmail.com",
                };
                await UserManager.CreateAsync(user3, "123abc");
            }
            await UserManager.AddToRoleAsync(user3, "Guest");
        }
    }
}
