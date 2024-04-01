using Instagram_Clone.Repositories.PostRepo;
using Instagram_Clone.Repositories.UserFollowRepo;
using Instagram_Clone.ViewModels;
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

        public ProfileController(Context context , IUserRelationshipRepository userRelationship , IPostRepository postRepository)
        {
            this.context = context;
            this.userRelationship = userRelationship;
            this.postRepository = postRepository;
        }
        public IActionResult Index()
        {
            //comment
            //comment2
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();

            string name = User.Identity.Name;
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.Bio=user.Bio;
            profileUserViewModel.Followers= userRelationship.GetFollowers(user.Id);
            profileUserViewModel.Following=userRelationship.GetFollowees(user.Id);
            profileUserViewModel.Posts = postRepository.GetAllPostsByUserID(user.Id);
            profileUserViewModel.ProfilePicture=user.ProfilePicture;
            return View("Index", profileUserViewModel);
        }

        public IActionResult Edit()
        {
            
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

                if (user == null)
                {
                    // Handle case when user is not found
                    return NotFound();
                }

                EditUserViewModel editUserViewMode = new EditUserViewModel();
                editUserViewMode.Id = user.Id;
                editUserViewMode.UserName = user.UserName;
                editUserViewMode.FirstName = user.FirstName;
                editUserViewMode.LastName = user.LastName;
                editUserViewMode.Email = user.Email;
                editUserViewMode.PhoneNumber = user.PhoneNumber;
                editUserViewMode.Bio = user.Bio;
                editUserViewMode.ProfilePicture = user.ProfilePicture;

                return View("Edit", editUserViewMode);
            
        }


        [HttpPost]
        public IActionResult Edit(EditUserViewModel editUserViewModel)
        {
            if (ModelState != null)
            {
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
               
                user.UserName = editUserViewModel.UserName;
                user.Email = editUserViewModel.Email;
                user.PhoneNumber = editUserViewModel.PhoneNumber;
                user.Bio = editUserViewModel.Bio;
                user.FirstName = editUserViewModel.FirstName;
                user.LastName = editUserViewModel.LastName;
                user.ProfilePicture = editUserViewModel.ProfilePicture;
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
