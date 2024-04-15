using Instagram_Clone.Authentication;
using Instagram_Clone.Hubs;
using Instagram_Clone.Models;
using Instagram_Clone.Repositories.NotificationRepo;
using Instagram_Clone.Repositories.UserFollowRepo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class FollowController : Controller
    {
       private IUserRelationshipRepository userRelationshipRepository;
       private readonly Context context;

        private readonly INotificationRepository<FollowRequest> notificationRepository;
        private readonly IHubContext<NotificationHub> hubContext;

        public FollowController( IUserRelationshipRepository _userRelationship , Context context, INotificationRepository<FollowRequest> _notificationRepository,  IHubContext<NotificationHub> _hubContext)
        {
            userRelationshipRepository = _userRelationship;
            this.context = context;

            notificationRepository = _notificationRepository;
            hubContext =_hubContext;



        }
        //public IActionResult AutocompleteSearch(string term)
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);

        //    // Replace this with your actual implementation to search for usernames based on the term
        //    List<string> userNames = SearchUserNames(term);

        //    return Json(userNames);
        //}

        //public IActionResult ShowFollowers(string Name)
        //{
        //    ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
           
        //    ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
        //    profileUserViewModel.UserName = user.UserName;
        //    profileUserViewModel.FirstName = user.FirstName;
        //    profileUserViewModel.LastName = user.LastName;
        //    profileUserViewModel.ProfilePicture = user.ProfilePicture;

        //    if (Name == null)
        //    {
        //        profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
        //        profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
        //    }
        //    else
        //    {
        //        profileUserViewModel.Followers = userRelationshipRepository.searchFollowers2(Name, user.Id);
        //        profileUserViewModel.Following = userRelationshipRepository.searchFollowees2(Name, user.Id);
        //    }

        //    return PartialView("_FollowersList", profileUserViewModel);
        //}


        //public ActionResult SearchFollower(string name)
        //{
        //    //Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    //ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    //ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
        //    //List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowers2(name,user.Id);
        //    //if (name != null)
        //    //{

        //    //    return PartialView("_FollowersList", searchedUsers);
        //    //}
        //    //else
        //    //{

        //    //    searchedUsers = new List<UserRelationship>();
        //    //    return PartialView("_FollowersList", searchedUsers);

        //    //}
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

        //    // Search for followers based on the provided name
        //    List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowers2(name, user.Id);
        //    ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
        //    //{
        //    //    // Assign properties accordingly
        //    //    // For example:
        //    //    Followers = searchedUsers,
        //    //    // Following = ...
        //    //};
        //    if (name!=null)
        //    {
        //        // Convert the searched users to the appropriate view model
        //        profileUserViewModel.Followers = searchedUsers;
        //        return PartialView("_FollowersList", profileUserViewModel);

        //    }
        //    else
        //    {
        //        profileUserViewModel.Followers = new List<UserRelationship>();
        //        return PartialView("_FollowersList", profileUserViewModel);
        //    }


        //    // Return the PartialView with the searched users
        //}
       

     
        public IActionResult Profile(string id)
        {
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
    
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == id);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.Email = user.Email;
            profileUserViewModel.IsDeleted = user.IsDeleted;
            profileUserViewModel.ProfilePicture=user.ProfilePicture;    
            profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
            profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            return View("Profile" ,profileUserViewModel);
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
                userRelationshipRepository.GetFollowingRelationship(id,user2.Id);
             
            }
            return RedirectToAction("Index","Profile");
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

        //public IActionResult GetFollowersAndFollowings()
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    List<UserRelationship> AllUsers = userRelationshipRepository.GetFollowersAndFollowings(user2.Id);
        //    return RedirectToAction("Index", "Home", AllUsers);
        //    //return PartialView("_DataBaseUsersPartial", AllUsers);
        //}

        //public IActionResult GetFollowersAndFollowings()
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    List<ApplicationUser> allUsers = userRelationshipRepository.GetFollowersAndFollowings(user.Id);
        //    return PartialView("_DataBaseUsersPartial", allUsers);
        //}

        //public IActionResult GetFollowersAndFollowings()
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    List<UserRelationship> AllUsers = userRelationshipRepository.GetFollowersAndFollowings(user2.Id);
        //    //return View("Index", AllUsers);
        //    List<UserRelationship> test = new List<UserRelationship>();
        //    test.Add(AllUsers.First());

        //    ViewBag.Users = test;
        //    return PartialView("_DataBaseUsersPartial");
        //}
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

            return  PartialView("_FollowersList", profileUserViewModel);
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


        //    /follow/FollowUser? id =
        //public IActionResult FollowUser(string id)
        //{
        //    //user1
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user1 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    List<UserRelationship> followersList = userRelationshipRepository.GetFollowers(id);
        //    string user1Name = user1.UserName;

        //    //Follow user
        //    ApplicationUser FollowUser = context.Users.FirstOrDefault(u => u.Id == id);
        //    string FollowUserName = FollowUser.UserName;


        //    if (followersList != null)
        //    {
        //        return View("");
        //    }

        //    else
        //    {
        //        //store these Data in VM
        //        UserRequestFollowVM userRequestFollowVM = new UserRequestFollowVM();
        //        userRequestFollowVM.userID = user1.Id;
        //        userRequestFollowVM.userName = user1Name;
        //        userRequestFollowVM.followID = id;
        //        userRequestFollowVM.followName = FollowUserName;

        //        return View("Request", userRequestFollowVM);
        //    }

        //}
        //public IActionResult FollowUser(string id)
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user1 = context.Users.FirstOrDefault(u => u.Id == claim.Value);

        //    // Check if user1 is already following the user with the given id
        //    bool isAlreadyFollowing = context.UserRelationship
        //        .Any(ur => ur.FolloweeId == id && ur.FollowerId == user1.Id && ur.IsDeleted == false);

        //    if (isAlreadyFollowing)
        //    {
        //        return View("AlreadyFollowingView");
        //    }
        //    else
        //    {
        //        // Fetch the user to follow
        //        ApplicationUser followUser = context.Users.FirstOrDefault(u => u.Id == id);

        //        if (followUser != null)
        //        {
        //            // Inside the FollowUser action where you create the UserRequestFollowVM instance
        //            UserRequestFollowVM userRequestFollowVM = new UserRequestFollowVM
        //            {
        //                userID = user1.Id,
        //                userName = user1.UserName,
        //                followID = id, // Ensure id is properly passed from the action parameter
        //                followName = followUser.UserName
        //            };

        //            return View("FollowUser", userRequestFollowVM);

        //        }
        //        else
        //        {
        //            // Handle case where followUser is not found
        //            return NotFound();
        //        }
        //    }
        //}
        public async Task<IActionResult> FollowUser(string id)
        {
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user1 = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            // Check if user1 is already following the user with the given id
            bool isAlreadyFollowing = context.UserRelationship
                .Any(ur => ur.FolloweeId == id && ur.FollowerId == user1.Id && ur.IsDeleted == false);

            if (isAlreadyFollowing)
            {
                return View("AlreadyFollowingView");
            }
            else
            {
                // Fetch the user to follow
                ApplicationUser followUser = context.Users.FirstOrDefault(u => u.Id == id);

                if (followUser != null)
                {
                    // Inside the FollowUser action where you create the UserRequestFollowVM instance
                    UserRequestFollowVM userRequestFollowVM = new UserRequestFollowVM
                    {
                        userID = user1.Id,
                        userName = user1.UserName,
                        followID = id, // Ensure id is properly passed from the action parameter
                        followName = followUser.UserName
                    };


                    // Create and save the notification
                    await notificationRepository.CreateNotification(user1.Id, id);

                    // Invoke the SignalR hub to send the notification
                    await hubContext.Clients.User(id).SendAsync("ReceiveNotification", user1.UserName + " Wants to follow you.");


                    return View("FollowUser", userRequestFollowVM);

                }
                else
                {
                    // Handle case where followUser is not found
                    return NotFound();
                }
            }
        }
        public IActionResult AcceptRequest(string followID, string userID)
        {
            // user
            ApplicationUser user1 = context.Users.FirstOrDefault(u => u.Id == userID);

            //Follow user
            ApplicationUser FollowUser = context.Users.FirstOrDefault(u => u.Id == followID);


            //Add relation 
            userRelationshipRepository.Follow(followID, userID);
            return RedirectToAction("Index", "Profile");
        }

    }
}
