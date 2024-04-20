using Instagram_Clone.Models.photo;
using Instagram_Clone.Repositories.CommentRepo;
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
        private readonly ICommentRepository commentRepository;

        public PostController(Context context,IPostRepository postRepository, IWebHostEnvironment webHostEnvironment, IpostPhotoRepository photoRepository , ICommentRepository commentRepository)
        {
            this.context = context;
            this.postRepository = postRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.photoRepository = photoRepository;
            this.commentRepository = commentRepository;
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


            // return View("SaveAdd", post);
            return RedirectToAction("Index", "Home");
        }

        //List<PostViewModel> posts = new List<PostViewModel>();
        //PostViewModel post = new PostViewModel();
        //post.Caption = "kerollos";
        //posts.Add(post);   
        //List<PostViewModel> postList = new List<PostViewModel>();
        //postList.Add(postViewModel);


        public IActionResult Comment(int postId)
        {


            Post? post = postRepository.GetPostwithUserAndCommentsAndFollowersById(postId);
            List<CommentViewModel> comments = new List<CommentViewModel>();

            if (post.Comments != null)
            {
                foreach (var comment in post.Comments)
                {
                    CommentViewModel commentView = new CommentViewModel();
                    commentView.ProfilePicture = comment.User.ProfilePicture.Name;
                    
                    commentView.Content = comment.Content;
                    commentView.UserName = comment.User.UserName;

                    TimeSpan TimeSincePost = DateTime.Now - comment.Date;
                    if (TimeSincePost.TotalSeconds < 60)
                        commentView.TimeAgo = $"{(int)TimeSincePost.TotalSeconds} second ago";
                    else if (TimeSincePost.TotalMinutes < 60)
                        commentView.TimeAgo = $"{(int)TimeSincePost.TotalMinutes} minute ago";
                    else if (TimeSincePost.TotalHours < 24)
                        commentView.TimeAgo = $"{(int)TimeSincePost.TotalHours} hour ago";
                    else
                        commentView.TimeAgo = $"{(int)TimeSincePost.TotalDays} day ago";

                    comments.Add(commentView);
                }

                

            }
            //ViewBag.picture = comments[0].ProfilePicture;
            return Json(comments);
           
        }

        //public IActionResult DeletePost(int postId)
        //{

        //}

        public IActionResult SaveComment(int postId , string comment)
        {

            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            Comment commentObj = new Comment
            {
                Content = comment,
                Date = DateTime.Now,
                UserId = user.Id,
                PostId = postId
            };

            commentRepository.Insert(commentObj);
            commentRepository.Save();

            return RedirectToAction("Index", "Home");
            
        }

        public IActionResult Delete(int postId)
        {
            Post? post = postRepository.GetPostByIDWithUser(postId);
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            if(post != null)
            {
                if(post.User.Id == user.Id)
                {
                    post.IsDeleted = true;
                    postRepository.Update(post);
                    postRepository.Save();
                    //return Json("deleted succss");

                    return Json("deleted success");
                }
                return Json("not match");
            }


            return Json("deleted not succss");
            //return View("_DeletePost");
        
        }
    }
}
