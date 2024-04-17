using Instagram_Clone.Models;
using Instagram_Clone.Repositories.MessageRepo;
using Instagram_Clone.Repositories.StoryRepo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instagram_Clone.Controllers
{
    public class StoryController: Controller
    {
        public readonly IStoryRepository storyRepository;
        public readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public StoryController(IWebHostEnvironment webHostEnvironment,IStoryRepository _storyRepository, UserManager<ApplicationUser> _userManager)
        {
            storyRepository = _storyRepository;
            userManager = _userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var userId = user.Id;

            var stories = storyRepository.GetAllStories(userId);

            return View(stories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormFile Image)
        {
            ApplicationUser currentUser = await userManager.GetUserAsync(User);

            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
            string name = Guid.NewGuid().ToString() + "_" + Image.FileName;
            string filePath = Path.Combine(uploadPath, name);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
            }

            var photo = new storyPhoto
            {
                Path = filePath,
                Name = name,
                IsDeleted = false
            };

            var story = new Story
            {
                CreatedAt = DateTime.Now,
                LifeTimeTicks = TimeSpan.FromHours(24).Ticks,
                IsDeleted = false,
                UserId = currentUser.Id,
                Photo = photo
            };

            storyRepository.Insert(story);
            storyRepository.Save();



            return View("saveAdd", story);
        }


    }
}
