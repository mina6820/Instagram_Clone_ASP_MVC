using Instagram_Clone.Repositories.PostRepo;
using Instagram_Clone.Repositories.UserFollowRepo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class FriendsProfileController : Controller
    {
        private readonly Context context;
        private readonly IUserRelationshipRepository userRelationship;
        private readonly IPostRepository postRepository;
        private readonly IWebHostEnvironment webHost;
        private readonly UserManager<ApplicationUser> userManager;

        public FriendsProfileController(Context context, IUserRelationshipRepository userRelationship, IPostRepository postRepository, IWebHostEnvironment webHost, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userRelationship = userRelationship;
            this.postRepository = postRepository;
            this.webHost = webHost;
            this.userManager = userManager;
        }
        public IActionResult Index(string ID)//(ProfileUserViewModel profileUserViewModel)
        {
           
            ApplicationUser user = context.Users
                .Include(u=>u.Followers)
                .Include(u=>u.Following)
                .Include(u=>u.ProfilePicture)
                .Include(u=>u.Posts)
                .FirstOrDefault(u => u.Id == ID);

            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            if (user != null)
            {
                profileUserViewModel.Id = user.Id;
                profileUserViewModel.UserName = user.UserName;
                profileUserViewModel.FirstName = user.FirstName;
                profileUserViewModel.LastName = user.LastName;
                profileUserViewModel.Bio = user.Bio;
                profileUserViewModel.ProfilePicture = user.ProfilePicture;
                profileUserViewModel.Followers = user.Followers;//user.Following;//user.Followers;// userRelationship.GetFollowers(user.Id);
                profileUserViewModel.Following = user.Following; //user.Followers;//user.Following;//userRelationship.GetFollowees(user.Id);
                profileUserViewModel.Posts = user.Posts;//postRepository.GetAllPostsByUserID(user.Id);
            }

            
            Claim claim2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim2.Value);
            ApplicationUser user3 = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);

            //UserRelationship ufollower = context.UserRelationship.FirstOrDefault(u=>u.FollowerId == user3.Id);
            //UserRelationship ufollowee = context.UserRelationship.FirstOrDefault(u => u.FolloweeId == user3.Id);

            //profileUserViewModel.Followers.Remove(ufollower);
            //profileUserViewModel.Following.Remove(ufollowee);

            List<ApplicationUser> NonFollowing = userRelationship.GetNonFollowees(user3.Id);     
            ViewBag.NonFollowingUsers = NonFollowing;

            List<ApplicationUser> Following = userRelationship.GetAppUserFollowees(user3.Id);
            ViewBag.FollowingUsers = Following;

            List<ApplicationUser> RequestedUsers = userRelationship.GetRequestedUsers(user3.Id);
            ViewBag.RequestedUsers = RequestedUsers;


            List<ApplicationUser> AllUsers = context.Users
                                            .Include(u => u.ProfilePicture)
                                            .ToList();


            //AllUsers.Remove(user2);

            ViewBag.Users = AllUsers;
            ViewBag.UserName = user3.UserName;
            ViewBag.picture = user3.ProfilePicture?.Name;

            return View("Index", profileUserViewModel);
            

            //return View("Index", "Profile");
        }
    
        public IActionResult ShowFollowers(string ID)
        {

            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser loginneduser = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            //ViewBag.UserName = loginneduser.UserName;
            
            ApplicationUser LoginnedUser2 = context.Users
                .Include(u => u.ProfilePicture)
                .FirstOrDefault(u => u.Id == loginneduser.Id);


          ApplicationUser user = context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .Include(u => u.ProfilePicture)
                .Include(u => u.Posts)
                .FirstOrDefault(u => u.Id == ID);

            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            if (user != null)
            {
                profileUserViewModel.Id = user.Id;
                profileUserViewModel.UserName = user.UserName;
                profileUserViewModel.FirstName = user.FirstName;
                profileUserViewModel.LastName = user.LastName;
                profileUserViewModel.Bio = user.Bio;
                profileUserViewModel.ProfilePicture = user.ProfilePicture;
                profileUserViewModel.Followers = user.Followers;//user.Following;//user.Followers;// userRelationship.GetFollowers(user.Id);
                profileUserViewModel.Following = user.Following;//user.Followers;//user.Following;//userRelationship.Get
                profileUserViewModel.ProfilePicture = user.ProfilePicture;
            }

            if (ID != null)
            {
                profileUserViewModel.Followers = userRelationship.GetFollowers(user.Id);
                profileUserViewModel.Following = userRelationship.GetFollowees(user.Id);
                
            }
            else
            {
                profileUserViewModel.Followers = userRelationship.searchFollowers2(user.UserName, user.Id);
                profileUserViewModel.Following = userRelationship.searchFollowees2(user.UserName, user.Id);
            }

            Claim claim2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim2.Value);
            ApplicationUser user3 = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            


            List<ApplicationUser> NonFollowingFriend = userRelationship.GetNonFollowees(user.Id);
            ViewBag.NonFollowingFriend = NonFollowingFriend;

            List<ApplicationUser> FollowingFriend = userRelationship.GetAppUserFollowees(user.Id);
            ViewBag.FollowingFriend = FollowingFriend;


            List<ApplicationUser> NonFollowingLoginnedUser = userRelationship.GetNonFollowees(user3.Id);
            ViewBag.NonFollowingLoginnedUser = NonFollowingLoginnedUser;

            List<ApplicationUser> FollowingLoginnedUser = userRelationship.GetAppUserFollowees(user3.Id);
            ViewBag.FollowingLoginnedUser = FollowingLoginnedUser;


            //var u= profileUserViewModel.Followers.Find(f=>f.Follower.Id==loginneduser.Id);
            //profileUserViewModel.Followers.Remove(u);
            //profileUserViewModel.Following.Remove(u);


            //List<ApplicationUser> Mutualusers= userRelationship.GetMutualFollowers(loginneduser.Id, user.Id);
            //List<ApplicationUser> NonMutualusers = userRelationship.GetNonMutualFollowers(loginneduser.Id, user.Id);

            return PartialView("_FollowersFriendPartial", profileUserViewModel);
        }

        


        public IActionResult ShowFollowees(string ID)
        {

            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser loginneduser = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ViewBag.UserName = loginneduser.UserName;
            ApplicationUser user = context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .Include(u => u.ProfilePicture)
                .Include(u => u.Posts)
                .FirstOrDefault(u => u.Id == ID);

            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            if (user != null)
            {
                profileUserViewModel.Id = user.Id;
                profileUserViewModel.UserName = user.UserName;
                profileUserViewModel.FirstName = user.FirstName;
                profileUserViewModel.LastName = user.LastName;
                profileUserViewModel.Bio = user.Bio;
                profileUserViewModel.ProfilePicture = user.ProfilePicture;
                profileUserViewModel.Followers = user.Following;//user.Followers;// userRelationship.GetFollowers(user.Id);
                profileUserViewModel.Following = user.Followers;//user.Following;//userRelationship.Get
                profileUserViewModel.ProfilePicture = user.ProfilePicture;
            }

            if (ID != null)
            {
                profileUserViewModel.Followers = userRelationship.GetFollowers(user.Id);
                profileUserViewModel.Following = userRelationship.GetFollowees(user.Id);
            }
            else
            {
                profileUserViewModel.Followers = userRelationship.searchFollowers2(user.UserName, user.Id);
                profileUserViewModel.Following = userRelationship.searchFollowees2(user.UserName, user.Id);
            }

            Claim claim2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim2.Value);
            ApplicationUser user3 = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);

            List<ApplicationUser> NonFollowingFriend = userRelationship.GetNonFollowees(user.Id);
            ViewBag.NonFollowingFriend = NonFollowingFriend;

            List<ApplicationUser> FollowingFriend = userRelationship.GetAppUserFollowees(user.Id);
            ViewBag.FollowingFriend = FollowingFriend;


            List<ApplicationUser> NonFollowingLoginnedUser = userRelationship.GetNonFollowees(user3.Id);
            ViewBag.NonFollowingLoginnedUser = NonFollowingLoginnedUser;

            List<ApplicationUser> FollowingLoginnedUser = userRelationship.GetAppUserFollowees(user3.Id);
            ViewBag.FollowingLoginnedUser = FollowingLoginnedUser;


            //var u = profileUserViewModel.Following.Find(f => f.Followee.Id == loginneduser.Id);
            //profileUserViewModel.Followers.Remove(u);
            //profileUserViewModel.Following.Remove(u);

            return PartialView("_FollowingFriendPartial", profileUserViewModel);
        }

        public ActionResult SearchFollower(string Name, string Id)
        {
            
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == Id);

            // Assuming you have a way to get followers from your user model

            // Assuming ProfileUserViewModel has a property named Followers
            var profileUserViewModel = new ProfileUserViewModel();

            if (Name != null)
            {
                var followers = userRelationship.searchFollowers2(Name, user.Id); // Replace this with your actual logic
                profileUserViewModel.Followers=followers;
            }
            else
            {
                profileUserViewModel.Followers = new List<UserRelationship>();
            }

            return PartialView("_FollowersFriendPartial", profileUserViewModel);
        }

        public ActionResult SearchFollowee(string Name, string Id)
        {
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == Id);

            // Assuming you have a way to get followers from your user model
            var followees = userRelationship.searchFollowees2(Name, user.Id); // Replace this with your actual logic

            // Assuming ProfileUserViewModel has a property named Followers
            var profileUserViewModel = new ProfileUserViewModel();

            if (Name != null)
            {
                profileUserViewModel.Following = followees;
            }
            else
            {
                profileUserViewModel.Following = new List<UserRelationship>();
            }
            return PartialView("_FollowingFriendPartial", profileUserViewModel);

        }


        //public IActionResult FollowingFilteration ()
        //{

        //}

    }
}
