using Instagram_Clone.Repositories.PostRepo;
using Microsoft.AspNetCore.Mvc;

namespace Instagram_Clone.Controllers
{
    public class ExploreController : Controller
    {
        public IPostRepository PostRepository;

        public ExploreController(IPostRepository _postRepository) 
        {
            PostRepository = _postRepository;
        }


        public IActionResult Index()
        {
           List<Post>? posts = PostRepository.GetAllPosts();

            List<string> paths = new List<string>();

            foreach(var post in posts)
            {
                paths.Add(post.PhotosPathes.First());
            }

            ViewBag.paths = paths;
            return View("Index", posts);
        }

        public IActionResult openPost(int postId)
        {
            Post post = PostRepository.GetPostwithUserAndCommentsAndFollowersById(postId);

            return RedirectToAction("Index", "Profile", new { UserId = post.UserId });
            //return View("openPost", post);
        }
    }
}
