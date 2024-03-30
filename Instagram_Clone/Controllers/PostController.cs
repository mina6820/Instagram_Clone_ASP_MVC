using Instagram_Clone.Models.photo;
using Instagram_Clone.Repositories.PhotoRepo;
using Instagram_Clone.Repositories.PostRepo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Instagram_Clone.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IPhotoRepository photoRepository;

        public PostController(IPostRepository postRepository, IWebHostEnvironment webHostEnvironment, IPhotoRepository photoRepository)
        {
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
            
            Post post = new Post();
            post.Caption = postVM.Caption;
            //post.Photos = new List<string> ();
            List<string> Pathes = new List<string>();
            Pathes.Add(filePath);

            post.PhotosPathes = Pathes;
            postRepository.Insert(post);
            postRepository.Save();

            postPhoto photo = new postPhoto();
            photo.Path = filePath;
            photo.Name = name;
            photo.PostId = post.Id;
            photoRepository.Insert(photo);
            photoRepository.Save();

            //foreach (var item in post.Photos)
            //{
            //    photoRepository.Insert(item);
            //    photoRepository.Save();
            //}


            return View("SaveAdd", post);

        }


        
    }
}
