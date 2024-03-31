using Instagram_Clone.Authentication;
using Instagram_Clone.Hubs;
using Instagram_Clone.Repositories.MessageRepo;
using Instagram_Clone.Repositories.PhotoRepo;
using Instagram_Clone.Repositories.UserFollowRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Instagram_Clone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<Context>();

            builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
            builder.Services.AddScoped<IUserRelationshipRepository,UserRelationshipRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddSignalR();


            //////////////////////////////==========buiild=================////////////////////////////////////////////////////////
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // The Hub
            app.MapHub<ChatHub>("/chat/index");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
