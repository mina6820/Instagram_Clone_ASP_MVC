using Instagram_Clone.Authentication;
using Instagram_Clone.Hubs;
using Instagram_Clone.Models;
using Instagram_Clone.Repositories.ChatRepo;
using Instagram_Clone.Repositories.NotificationRepo;

//using Instagram_Clone.Repositories.NotificationRepo;
using Instagram_Clone.Repositories.UserFollowRepo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class FollowController : Controller
    {
        private IUserRelationshipRepository userRelationshipRepository;
        private readonly Context context;

        //private readonly IChatRepository chatRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationRepository notificationRepository;
        private readonly IChatRepository chatRepository;
        private readonly IHubContext<NotificationHub> _notificationHub;
        //private readonly INotificationRepository _notificationRepository;



        public FollowController(IHubContext <NotificationHub> notificationHub ,
            IUserRelationshipRepository userRelationshipRepository, Context context,
                            UserManager<ApplicationUser> userManager
                            , INotificationRepository  notificationRepository,
                            IChatRepository chatRepository
            )

        {
            this.userRelationshipRepository = userRelationshipRepository;
            this.context = context;

            _userManager = userManager;
            this.notificationRepository = notificationRepository;
            this.chatRepository = chatRepository;
            _notificationHub = notificationHub;
            //chatRepository = _chatRepository;
            //_notificationRepository = notificationRepository;


        }


        public async Task<IActionResult> FollowUser(string id)
        {
            try
            {
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser sender = await _userManager.FindByIdAsync(claim.Value);
                ApplicationUser receiver = await _userManager.FindByIdAsync(id);

                if (receiver == null)
                {
                    return NotFound();
                }

                bool isAlreadyFollowing = await userRelationshipRepository.IsFollowing(sender.Id, receiver.Id);
                if (!isAlreadyFollowing)
                {
                    // Follow the user asynchronously
                    //await userRelationshipRepository.Accept_FollowRequest(receiver.Id, sender.Id);


                    // Add a follow notification
                    //await _notificationRepository.AddNotificationAsync(new Notification
                    //{
                    //    NotificationType = NotificationType.FollowRequest,
                    //    SenderId = sender.Id,
                    //    ReceiverId = receiver.Id,
                    //    Date = DateTime.Now
                    //});

                    // Create and save the FollowRequest_notification directly
                    var followRequest = new FollowRequest_notification
                    {
                        SenderId = sender.Id,
                        ReceiverId = receiver.Id,
                        IsAccepted = false, // Set as needed
                        IsRequested = true
                    };
                     context.FollowRequest_notifications.Add(followRequest);
                    await context.SaveChangesAsync();


                    // Send a follow notification via SignalR

                    //await _notificationHub.SendFollowNotification(receiver.Id, sender.UserName);senderUserName, , followRequest.Id,receiver.Id,sender.Id
                    await _notificationHub.Clients.User(receiver.Id).SendAsync("ReceiveFollowNotification",sender.UserName, followRequest.Id, receiver.Id, sender.Id);

                    // Redirect to home or another appropriate action
                    return RedirectToAction("Index", "Home");
                }
                return View("AlreadyFollowingView");
            }
            catch (Exception ex)
            {
                // Handle the exception
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> AcceptRequest(int NotificationID, string receiverId, string senderId)
        {
            await userRelationshipRepository.Accept_FollowRequest(receiverId, senderId);
            notificationRepository.Hide_Request(NotificationID);

            var chat = new Chat { SenderId = senderId, RecieverId = receiverId };

            try
            {
                chatRepository.Insert(chat);
                chatRepository.Save();
            }
            catch (DbUpdateException ex) when ((ex.InnerException as SqlException)?.Number == 2627)
            {
                // Unique constraint violation, chat already exists
                // Do nothing, as you don't want to take any action
            }
            catch (Exception ex)
            {
                // Handle other exceptions
            }
            return RedirectToAction("Index", "Profile");
        }


        public async Task<IActionResult> Follow_Back(int NotificationID , string followeeid, string loginuser)
        {

            await userRelationshipRepository.Followback(followeeid, loginuser);
            notificationRepository.Hide_Request(NotificationID);
            // return View("");
            return RedirectToAction("Index", "Profile");
        }
          
        public  IActionResult Reject_Request(int NotificationID)
        {
             notificationRepository.Hide_Request(NotificationID);
            // return View("");
            return RedirectToAction("Index", "Profile");
        }


        public IActionResult Profile(string id)
        {
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();

            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == id);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.Email = user.Email;
            profileUserViewModel.IsDeleted = user.IsDeleted;
            profileUserViewModel.ProfilePicture = user.ProfilePicture;
            profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
            profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            return View("Profile", profileUserViewModel);
        }

        public IActionResult UnFollow(string id)
        {
            // user.id (login user)
            // id (id of the follower)
            if (id != null)
            {
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
                //ViewBag.LoggedInUserId= user2.Id;
                userRelationshipRepository.GetFollowingRelationship(id, user2.Id);

            }
            return RedirectToAction("Index", "Profile");
        }

        public IActionResult RemoveFollower(string id)
        {
            // id (login user)
            //  user.id (id of the follower)
            if (id != null)

            {
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
                userRelationshipRepository.GetFollowersRelationship(id, user2.Id);

            }
            return RedirectToAction("Index", "Profile");
        }

      
        //habeba and messi 

        public IActionResult ShowFollowers(string Name)
        {
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.ProfilePicture = user.ProfilePicture;

            if (Name == null)
            {
                profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
                profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            }
            else
            {
                profileUserViewModel.Followers = userRelationshipRepository.searchFollowers2(Name, user.Id);
                profileUserViewModel.Following = userRelationshipRepository.searchFollowees2(Name, user.Id);
            }

            return PartialView("_FollowersList", profileUserViewModel);
        }

        public IActionResult showFollowees(string Name)
        {
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.ProfilePicture = user.ProfilePicture;

            if (Name == null)
            {
                profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
                profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            }
            else
            {
                profileUserViewModel.Followers = userRelationshipRepository.searchFollowers2(Name, user.Id);
                profileUserViewModel.Following = userRelationshipRepository.searchFollowees2(Name, user.Id);
            }

            return PartialView("_FollowingList", profileUserViewModel);
        }

        public ActionResult SearchFollower(string name)
        {
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();

            if (name != null)
            {
                // Search for followers based on the provided name
                List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowers2(name, user.Id);
                profileUserViewModel.Followers = searchedUsers ?? new List<UserRelationship>();
            }
            else
            {
                profileUserViewModel.Followers = new List<UserRelationship>();
            }

            return PartialView("_FollowersList", profileUserViewModel);
        }

        public ActionResult SearchFollowee(string name)
        {
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            //ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            //ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            //List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowees2(name, user.Id);
            if (name != null)
            {
                List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowees2(name, user.Id);
                profileUserViewModel.Following = searchedUsers ?? new List<UserRelationship>();


                // return PartialView("_FollowingList", searchedUsers);
            }
            else
            {
                profileUserViewModel.Following = new List<UserRelationship>();

                //searchedUsers = new List<UserRelationship>();
                //return PartialView("_FollowingList", searchedUsers);
            }
            return PartialView("_FollowingList", profileUserViewModel);

        }

       
    }
    //}
}


