using Instagram_Clone.Models;
using Instagram_Clone.Repositories.PostRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Instagram_Clone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository postRepository;

        /// <summary>
        /// messi
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger , IPostRepository postRepository)
        {
            _logger = logger;
            this.postRepository = postRepository;
        }

        public IActionResult Index()
        {
            List<Post> posts = postRepository.GetAllPostsWithPhotosAndLikes();
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
                    // Handle the case where post.User is null
                    postViewModel.UserName = "Unknown"; // Or any default value you prefer
                }
                postViewModel.ImagesNames = post.PhotosPathes;
                postViewModel.Likes = post.Likes.Count();

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
