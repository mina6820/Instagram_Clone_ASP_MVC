using Instagram_Clone.Authentication;
using Instagram_Clone.Hubs;
using Instagram_Clone.Repositories.ChatRepo;
using Instagram_Clone.Repositories.CommentRepo;
using Instagram_Clone.Repositories.LikeRepo;
using Instagram_Clone.Repositories.MessageRepo;
//using Instagram_Clone.Repositories.NotificationRepo;
using Instagram_Clone.Repositories.PhotoRepo;
using Instagram_Clone.Repositories.PhotoRepo.message;
using Instagram_Clone.Repositories.PhotoRepo.postPhotoContainer;
using Instagram_Clone.Repositories.PhotoRepo.ProfilePhotoContainer;
using Instagram_Clone.Repositories.PhotoRepo.Story;
using Instagram_Clone.Repositories.PostRepo;
using Instagram_Clone.Repositories.StoryRepo;
using Instagram_Clone.Repositories.StoryViewRepo;
using Instagram_Clone.Repositories.UserFollowRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Hosting;
using Instagram_Clone.Repositories.NotificationRepo;
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
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
            builder.Services.AddScoped<IprofilePhotoRepository, profilePhotoRepository>();
            builder.Services.AddScoped<IpostPhotoRepository, postPhotoRepository>();
            builder.Services.AddScoped<ImessagePhotoRepository, messagePhotoRepository>();
            builder.Services.AddScoped<IstoryPhotoRepository, storyPhotoRepository>();
            builder.Services.AddScoped<IUserRelationshipRepository,UserRelationshipRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ILikeRepository, LikeRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IStoryRepository, StoryRepository>();
            builder.Services.AddScoped<IStoryViewRepository, StoryViewRepository>();
            builder.Services.AddScoped<IpostPhotoRepository, postPhotoRepository>();
            builder.Services.AddScoped<IChatRepository, ChatRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

            // builder.Services.AddScoped<IFollowRequestRepository,FollowRequestRepository>();
            builder.Services.AddScoped<NotificationHub>();
            //builder.Services.AddSingleton<NotificationHub>();


            builder.Services.AddHostedService<StoryExpirationService>();


            builder.Services.AddSignalR();
            //builder.Services.AddSignalR().AddHubOptions<NotificationHub>(options =>
            //{
            //    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            //});

            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();




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
            app.MapHub<ChatterHub>("/ChatH");
            app.MapHub<PostHub>("/PostH");
            app.MapHub<NotificationHub>("/NotificationHub");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
