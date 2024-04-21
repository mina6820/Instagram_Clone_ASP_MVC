using Instagram_Clone.Repositories.NotificationRepo;
using Instagram_Clone.Repositories.PostRepo;
using Instagram_Clone.Repositories.StoryRepo;
using Instagram_Clone.Repositories.UserFollowRepo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository postRepository;
        private readonly IStoryRepository storyRepository;
        private readonly Context context;
        private IUserRelationshipRepository userRelationshipRepository;
        private INotificationRepository notificationRepository;

        /// <summary>
        /// messi
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(IStoryRepository _storyRepository,ILogger<HomeController> logger , IPostRepository postRepository, Context context, IUserRelationshipRepository _userRelationship , INotificationRepository notificationRepository)
        {
            _logger = logger;
            this.postRepository = postRepository;
            this.context = context;
            userRelationshipRepository = _userRelationship;
            storyRepository = _storyRepository;
            this.notificationRepository= notificationRepository;
        }

        public IActionResult Index()
        {
            Claim? claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            List<Post> myPosts = postRepository.GetMyPosts(user.Id);
            List<Post> posts = postRepository.GetAllPostsWithPhotosAndLikes(user.Id);
            List<PostViewModel> postsViewModel = new List<PostViewModel>();
            var finalPosts = posts.Concat(myPosts);
            foreach (Post post in finalPosts)
            {
                PostViewModel postViewModel = new PostViewModel();
                postViewModel.Caption = post.Caption;
                if (post.User != null)
                {
                    postViewModel.UserName = post.User.UserName;
                    postViewModel.ProfilePhoto = post.User.ProfilePicture;
                }
                else
                {
                    // Handle the case where post.User is null // remember to remove this
                    postViewModel.UserName = "Unknown"; // Or any default value you prefer
                }
                postViewModel.ID = post.Id;
                postViewModel.ImagesNames = post.PhotosPathes;
                postViewModel.Likes = post.Likes;
                postViewModel.Comments = post.Comments;
                postViewModel.CreatedAt = post.Date;
                postViewModel.UserId = post.User.Id;

                ViewBag.CurrentUserId = user?.Id;

                TimeSpan TimeSincePost = DateTime.Now - post.Date;
                if (TimeSincePost.TotalSeconds < 60)
                    postViewModel.TimeAgo = $"{(int)TimeSincePost.TotalSeconds} second ago";
                else if (TimeSincePost.TotalMinutes < 60)
                    postViewModel.TimeAgo = $"{(int)TimeSincePost.TotalMinutes} minute ago";
                else if (TimeSincePost.TotalHours < 24)
                    postViewModel.TimeAgo = $"{(int)TimeSincePost.TotalHours} hour ago";
                else
                    postViewModel.TimeAgo = $"{(int)TimeSincePost.TotalDays} day ago";

                postsViewModel.Add(postViewModel);

            }
            // ViewBag.postsList = postsViewModel;
            //ViewBag.myPosts = myPosts;

            ViewBag.postsList = postsViewModel.OrderByDescending(post => post.CreatedAt);
            //habeba



            Claim claim2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim2.Value);
            ApplicationUser user3 = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);


            List<ApplicationUser> NonFollowing = userRelationshipRepository.GetNonFollowees(user.Id);
            ViewBag.NonFollowingUsers = NonFollowing;

            List<ApplicationUser> applicationUsers = userRelationshipRepository.GetRandomlyTopFive(user2.Id);
            ViewBag.applicationUsers = applicationUsers;


            List<ApplicationUser> AllUsers = context.Users
                .Include(u => u.ProfilePicture)
                .ToList();


            AllUsers.Remove(user2);

            ViewBag.Users = AllUsers;
            ViewBag.UserName = user3.UserName;
            ViewBag.picture = user3.ProfilePicture?.Name;



            // Story
            var userId = user.Id;

            var myStories = storyRepository.GetMyStories(userId);

            var stories = storyRepository.GetAllStories(userId);

            ViewBag.myStories = myStories;
            ViewBag.Stories = stories;


            //Notifications
            List<FollowRequest_notification> notifications = notificationRepository.GetNotifications(user3.Id);
            ViewBag.notifications = notifications;
            return View();


        }

        public IActionResult Comment(int postId)
        {


            Post? post = postRepository.GetPostwithUserAndCommentsAndFollowersById(postId);
            List<CommentViewModel> comments = new List<CommentViewModel>();

            if (post.Comments != null)
            {
                foreach (var comment in post.Comments)
                {
                    CommentViewModel commentView = new CommentViewModel();
                    commentView.ProfilePicture = comment.User.ProfilePicture.Path;
                    commentView.Content = comment.Content;
                    commentView.UserName = comment.User.UserName;
                    comments.Add(commentView);
                }



            }
            ViewBag.Comments = comments;



            return PartialView("_CommentPartial");




        }


        //public IActionResult SearchUsers(string Name)
        //{
        //    Claim claim2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim2.Value);
        //    ApplicationUser user3 = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);

        //    List<ApplicationUser> AllUsers = context.Users
        //        .Include(u => u.ProfilePicture)
        //        //.Include(u=>u.)
        //        .ToList();


        //    AllUsers.Remove(user2);

        //    List<ApplicationUser> searchResults = AllUsers;

        //    if (Name != null)
        //    {
        //        searchResults = AllUsers
        //            .Where(u => u.UserName.Contains(Name)) // Example search logic, modify according to your requirements
        //            .ToList();
        //    }
           
        //    return View("SearchUsers", searchResults);
        //}

        public IActionResult GoToAllUsers(string Name)
        {

            Claim claim2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim2.Value);
            ApplicationUser user3 = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);

            List<ApplicationUser> AllUsers = context.Users
                .Include(u => u.ProfilePicture)
                .ToList();
            AllUsers.Remove(user2);
            //ViewBag.Users = AllUsers;

            List<ApplicationUser> searchResults = AllUsers;

            
            if (Name == null)
            {
                return View("GoToAllUsers", AllUsers);
            }
            else
            {
                searchResults = AllUsers
                    .Where(u => u.UserName.Contains(Name)) // Example search logic, modify according to your requirements
                    .ToList();
                return View("GoToAllUsers", searchResults);
            }
            //ViewBag.UserName = user2.UserName;
            //return View("GoToAllUsers", AllUsers);
        }

        //public IActionResult GetNonFollowees()
        //{
        //    Claim claim2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim2.Value);
        //    List<UserRelationship> applicationUsers = userRelationshipRepository.GetNonFollowees(user2.Id);
        //    return PartialView("_SideBarPartial", applicationUsers);
        //}




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult Posts()
        //{
            //List<Post> posts = postRepository.GetAllPostsWithPhotosAndLikes();
            //List<PostViewModel> postsViewModel = new List<PostViewModel>();
            //foreach (Post post in posts)
            //{
            //    PostViewModel postViewModel = new PostViewModel();
            //    postViewModel.Caption = post.Caption;
            //    if (post.User != null)
            //    {
            //        postViewModel.UserName = post.User.UserName;
            //        postViewModel.ProfilePhoto = post.User.ProfilePicture;
            //    }
            //    else
            //    {
            //        // Handle the case where post.User is null // remember to remove this
            //        postViewModel.UserName = "Unknown"; // Or any default value you prefer
            //    }
            //    postViewModel.ID = post.Id;
            //    postViewModel.ImagesNames = post.PhotosPathes;
            //    postViewModel.Likes = post.Likes;
            //    postViewModel.Comments = post.Comments;
            //    postViewModel.CreatedAt = post.Date;
            //    //postViewModel.ProfilePhoto = post.User.ProfilePicture;


            //    // get the current user
            //    Claim? claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            //    ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            //    ViewBag.CurrentUserId = user?.Id;

            //    TimeSpan TimeSincePost = DateTime.Now - post.Date;
            //    if (TimeSincePost.TotalSeconds < 60)
            //        postViewModel.TimeAgo = $"{(int)TimeSincePost.TotalSeconds} second ago";
            //    else if (TimeSincePost.TotalMinutes < 60)
            //        postViewModel.TimeAgo = $"{(int)TimeSincePost.TotalMinutes} minute ago";
            //    else if (TimeSincePost.TotalHours < 24)
            //        postViewModel.TimeAgo = $"{(int)TimeSincePost.TotalHours} hour ago";
            //    else
            //        postViewModel.TimeAgo = $"{(int)TimeSincePost.TotalDays} day ago";

            //    //postViewModel.TimeAgo = post.Date;


            //    postsViewModel.Add(postViewModel);
            //}
            //ViewBag.postsList = postsViewModel;
            //return View();
        //}



        //public IActionResult Index()
        //{
        //    return View();
        //}

    }
}
