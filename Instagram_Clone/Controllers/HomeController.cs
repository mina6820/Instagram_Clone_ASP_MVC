using Instagram_Clone.Models;
using Instagram_Clone.Repositories.PostRepo;
using Instagram_Clone.Repositories.UserFollowRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository postRepository;
        private readonly Context context;
        private IUserRelationshipRepository userRelationshipRepository;


        /// <summary>
        /// messi
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger , IPostRepository postRepository, Context context, IUserRelationshipRepository _userRelationship)
        {
            _logger = logger;
            this.postRepository = postRepository;
            this.context = context;
            userRelationshipRepository = _userRelationship;

        }

        public IActionResult Index()
            {
            // get the current user
            Claim? claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            List<Post> posts = postRepository.GetAllPostsWithPhotosAndLikes(user.Id);
            List<PostViewModel> postsViewModel = new List<PostViewModel>();
            foreach (Post post in posts)
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
                //postViewModel.ProfilePhoto = post.User.ProfilePicture;


               
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

                //postViewModel.TimeAgo = post.Date;


                postsViewModel.Add(postViewModel);
            }
            ViewBag.postsList = postsViewModel;

            //habeba



            Claim claim2 = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim2.Value);
            ApplicationUser user3 = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);


            List<ApplicationUser> AllUsers = context.Users
                .Include(u=>u.ProfilePicture)
                .ToList();

            AllUsers.Remove(user2);

            //List<ApplicationUser> allUsers = userRelationshipRepository.GetFollowersAndFollowings(user3.Id);
            //return View("Index", AllUsers);
            //List<UserRelationship> test = new List<UserRelationship>();
            //test.Add(AllUsers.First());

            ViewBag.Users = AllUsers;
            ViewBag.UserName = user3.UserName;

            // wessa code 

            //Post? post2 = postRepository.GetPostwithUserAndCommentsAndFollowersById(postId);
            //List<CommentViewModel>? comments = new List<CommentViewModel>();
            //List<Comment>? listcomments = post2?.Comments;

            //if (post2 != null && post2.Comments != null)
            //{
            //    foreach (var comment in listcomments)
            //    {
            //        CommentViewModel commentView = new CommentViewModel();
            //        commentView.ProfilePicture = comment.User.ProfilePicture;
            //        commentView.Content = comment.Content;
            //        commentView.UserName = comment.User.UserName;
            //        comments.Add(commentView);
            //    }


            //}

            //ViewBag.Comments = comments;

            return View("index");

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



            //List<int> nums = new List<int>() { 1,2,3,4,5};
            //ViewBag.Nums = nums;

            //ViewData["Nums"] = nums;

            //PostViewModel postViewModel = new PostViewModel();
            //postViewModel.UserName = post.User.UserName;
            //postViewModel.ProfilePhoto = post.User.ProfilePicture;
            //postViewModel.Comments = post.Comments;
            //postViewModel.Followers = post.User.Followers;

            //List<PostViewModel> posts = new List<PostViewModel>();  
            //posts.Add(postViewModel);


            return PartialView("_CommentPartial");
            // return RedirectToAction("Home/Index");

        }


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
