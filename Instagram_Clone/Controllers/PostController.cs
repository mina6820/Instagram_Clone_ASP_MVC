using Instagram_Clone.Models.photo;
using Instagram_Clone.Repositories.PhotoRepo;
using Instagram_Clone.Repositories.PhotoRepo.postPhotoContainer;
using Instagram_Clone.Repositories.PostRepo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class PostController : Controller
    {
        private readonly Context context;
        private readonly IPostRepository postRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IpostPhotoRepository photoRepository;

        public PostController(Context context,IPostRepository postRepository, IWebHostEnvironment webHostEnvironment, IpostPhotoRepository photoRepository)
        {
            this.context = context;
            this.postRepository = postRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.photoRepository = photoRepository;
        } 
        public IActionResult Add()
        {
            return View("Add");
        }


        public IActionResult SaveAdd(AddPostVM postVM)
        {

            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
            string name = Guid.NewGuid().ToString() + "_" + postVM.Image.FileName;
            string filePath = Path.Combine(uploadPath, name);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                postVM.Image.CopyTo(fileStream);
            }

            //photo.Path = name;

            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            Post post = new Post();
            post.Caption = postVM.Caption;
            //post.Photos = new List<string> ();
            List<string> Pathes = new List<string>();
            Pathes.Add(filePath);

            post.PhotosPathes.Add(name);
            post.UserId = user.Id;
            postRepository.Insert(post);
            postRepository.Save();

            postPhoto photo = new postPhoto();
            photo.Path = filePath;
            photo.Name = name;
            photo.PostId = post.Id;
            photoRepository.Insert(photo);
            photoRepository.Save();
            ViewBag.Name = name;

            //foreach (var item in post.Photos)
            //{
            //    photoRepository.Insert(item);
            //    photoRepository.Save();
            //}

            //< img src = "D:\Mvc\project\Instagram_Clone\wwwroot\images\71ed0bca-c360-4885-ac83-458d38a18d3e_oii.jpeg" style = "width:100px; height:100px;" >


            return View("SaveAdd", post);

        }

        //List<PostViewModel> posts = new List<PostViewModel>();
        //PostViewModel post = new PostViewModel();
        //post.Caption = "kerollos";
        //posts.Add(post);   
        //List<PostViewModel> postList = new List<PostViewModel>();
        //postList.Add(postViewModel);
        //public IActionResult Comment(int postId)
        //{
            

        //    Post? post =  postRepository.GetPostwithUserAndCommentsAndFollowersById(postId);
        //    List<CommentViewModel> comments = new List<CommentViewModel>();

        //    if(post.Comments != null)
        //    {
        //        foreach (var comment in post.Comments)
        //        {
        //            CommentViewModel commentView = new CommentViewModel();
        //            commentView.ProfilePicture = comment.User.ProfilePicture;
        //            commentView.Content = comment.Content;
        //            commentView.UserName = comment.User.UserName;
        //            comments.Add(commentView);
        //        }

        //        ViewBag.Comments = comments;
                
        //    }


        //    //PostViewModel postViewModel = new PostViewModel();
        //    //postViewModel.UserName = post.User.UserName;
        //    //postViewModel.ProfilePhoto = post.User.ProfilePicture;
        //    //postViewModel.Comments = post.Comments;
        //    //postViewModel.Followers = post.User.Followers;

        //    //List<PostViewModel> posts = new List<PostViewModel>();  
        //    //posts.Add(postViewModel);


        //    //return PartialView("_CommentPartial");
        //    return View("test");
        //}


        public IActionResult SaveComment()
        {
            return Content("success");
        }
    }
}
