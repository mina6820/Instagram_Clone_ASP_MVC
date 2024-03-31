using Instagram_Clone.Repositories.UserFollowRepo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class ProfileController : Controller
    {
        private readonly Context context;
        private readonly IUserRelationshipRepository userRelationship;

        public ProfileController(Context context , IUserRelationshipRepository userRelationship)
        {
            this.context = context;
            this.userRelationship = userRelationship;
        }
        public IActionResult Index()
        {
            //comment
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            string name = User.Identity.Name;
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.Followers= userRelationship.GetFollowers(user.Id);
            profileUserViewModel.Following=userRelationship.GetFollowees(user.Id);
            return View("Index", profileUserViewModel);
        }
    }
}
