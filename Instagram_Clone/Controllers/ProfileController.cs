using Instagram_Clone.Repositories.PostRepo;
using Instagram_Clone.Repositories.UserFollowRepo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class ProfileController : Controller
    {
        private readonly Context context;
        private readonly IUserRelationshipRepository userRelationship;
        private readonly IPostRepository postRepository;
        private readonly IWebHostEnvironment webHost;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileController(Context context , IUserRelationshipRepository userRelationship , IPostRepository postRepository , IWebHostEnvironment webHost , UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userRelationship = userRelationship;
            this.postRepository = postRepository;
            this.webHost = webHost;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string UserId)
        {
            //comment
            //comment2
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            ApplicationUser user = new ApplicationUser();
            if (UserId == null)
            {
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
                 user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            }
            else
            {
                ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == UserId);
                 user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            }
            
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.Bio=user.Bio;
            profileUserViewModel.Followers= userRelationship.GetFollowers(user.Id);
            profileUserViewModel.Following=userRelationship.GetFollowees(user.Id);
            profileUserViewModel.Posts = postRepository.GetAllPostsByUserID(user.Id);
            //profileUserViewModel.ProfilePicture = user.ProfilePicture;

            //if (user.ProfilePicture == null)
            //{
            //    profilePhoto profilePhoto = new profilePhoto();
            //    profilePhoto.Name = "messi.jpg";
            //    profilePhoto.Path = Path.Combine(webHost.WebRootPath, "Images");
            //    profilePhoto.UserId = user.Id;
            //    profilePhoto.User = user;

            //    user.ProfilePicture = profilePhoto;
            //}
                profileUserViewModel.ProfilePicture=user.ProfilePicture;
            return View("Index", profileUserViewModel);
        }

        [HttpGet]
        public IActionResult Edit()
        {

            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);

            if (user == null)
            {
                // Handle case when user is not found
                return NotFound();
            }

            ProfileUserViewModel editUserViewMode = new ProfileUserViewModel();
            editUserViewMode.Id = user.Id;
            editUserViewMode.UserName = user.UserName;
            editUserViewMode.FirstName = user.FirstName;
            editUserViewMode.LastName = user.LastName;
            editUserViewMode.Email = user.Email;
            editUserViewMode.PhoneNumber = user.PhoneNumber;
            editUserViewMode.Bio = user.Bio;
            
            //editUserViewMode.CurrentPassword=user.PasswordHash;
            //if (user.ProfilePicture == null)
            //{
            //    profilePhoto profilePhoto = new profilePhoto();
            //    profilePhoto.Name = "messi.jpg";
            //    profilePhoto.Path = Path.Combine(webHost.WebRootPath, "Images");
            //    profilePhoto.UserId = user.Id;
            //    profilePhoto.User = user;

            //    user.ProfilePicture = profilePhoto;
            //}
            editUserViewMode.ImgName = user.ProfilePicture?.Name;
            return View("Edit", editUserViewMode);

        }


        [HttpPost]
        public async Task <IActionResult> Edit(EditUserViewModel editUserViewModel)
        {
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);

            string wwrootPath = Path.Combine(webHost.WebRootPath, "Images");
            if (editUserViewModel.ProfilePicture == null)
            {
                user.UserName = editUserViewModel.UserName;
                user.Email = editUserViewModel.Email;
                user.PhoneNumber = editUserViewModel.PhoneNumber;
                user.Bio = editUserViewModel.Bio;
                user.FirstName = editUserViewModel.FirstName;
                user.LastName = editUserViewModel.LastName;

                if(editUserViewModel.CurrentPassword != null)
                {
                    if (await userManager.CheckPasswordAsync(user, editUserViewModel.CurrentPassword))
                    {
                        await userManager.ChangePasswordAsync(user, editUserViewModel.CurrentPassword, editUserViewModel.NewPassword);
                    }
                }
                
                context.Users.Update(user);

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            string imageName = Guid.NewGuid().ToString() + "_" + editUserViewModel.ProfilePicture.FileName;
            string filePath = Path.Combine(wwrootPath, imageName);
            

            using (FileStream fileStream = new FileStream(filePath, FileMode.Append))
            {
                editUserViewModel.ProfilePicture.CopyTo(fileStream);
            }
            if (ModelState != null)
            {
                
               
                user.UserName = editUserViewModel.UserName;
                user.Email = editUserViewModel.Email;
                user.PhoneNumber = editUserViewModel.PhoneNumber;
                user.Bio = editUserViewModel.Bio;
                user.FirstName = editUserViewModel.FirstName;
                user.LastName = editUserViewModel.LastName;
                profilePhoto profileImage = new profilePhoto();
                profileImage.Name = imageName;
                profileImage.Path = filePath;
                profileImage.UserId = user.Id;
                profileImage.User= user;
                user.ProfilePicture = profileImage; //editUserViewModel.ProfilePicture.Name;

                if (editUserViewModel.CurrentPassword != null && editUserViewModel.ConfirmPassword!=null && editUserViewModel.NewPassword!=null)
                {
                    if (await userManager.CheckPasswordAsync(user, editUserViewModel.CurrentPassword))
                    {
                        await userManager.ChangePasswordAsync(user, editUserViewModel.CurrentPassword, editUserViewModel.NewPassword);
                    }
                }
                context.Users.Update(user);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View("Edit",editUserViewModel);
            }
        }

    }
}
