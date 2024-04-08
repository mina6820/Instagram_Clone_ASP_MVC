using Instagram_Clone.Models;
using Instagram_Clone.Repositories.PostRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// messi
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger , IPostRepository postRepository, Context context)
        {
            _logger = logger;
            this.postRepository = postRepository;
            this.context = context;
        }

        public IActionResult Index()
        {
            // get the current user
            Claim? claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            List<Post> posts = postRepository.GetAllPostsWithPhotosAndLikes(user.Id);
            List<PostViewModel> postsViewModel = new List<PostViewModel>();
            foreach(Post post in posts)
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
            return View(postsViewModel);
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
    }
}
