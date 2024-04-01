using Instagram_Clone.Authentication;
using Instagram_Clone.Models;
using Instagram_Clone.Repositories.UserFollowRepo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class FollowController : Controller
    {
       private IUserRelationshipRepository userRelationshipRepository;
       private readonly Context context;

        public FollowController( IUserRelationshipRepository _userRelationship , Context context)
        {
            userRelationshipRepository = _userRelationship;
            this.context = context;
        }


        public IActionResult ShowFollowers(string Name)
        {
         
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).Include(u=>u.Followers).FirstOrDefault(u => u.Id == user2.Id);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.ProfilePicture = user.ProfilePicture;
            ViewBag.FollowersImages = user.Followers;
            if (Name == null)
            {
                profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
                profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            }
            else
            {
                ViewBag.searchFollowers = userRelationshipRepository.searchFollowers(Name);
                //profileUserViewModel.Followers = userRelationshipRepository.searchFollowers(Name);
                profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            }

            return PartialView("_FollowersList", profileUserViewModel);
        }

        //Follow/showFollowees?id=
        //public ActionResult showFollowees()//string id)
        //{
        //    //List<UserRelationship> Followees = userRelationshipRepository.GetFollowees(id);
        //    ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
        //    //string name = User.Identity.Name;
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    profileUserViewModel.UserName = user.UserName;
        //    profileUserViewModel.FirstName = user.FirstName;
        //    profileUserViewModel.LastName = user.LastName;
        //    profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
        //    profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
        //    return View("showFollowees", profileUserViewModel);
        //}

        //[HttpGet]
        //public IActionResult SearchFollower(string name)
        //{
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        // Perform search based on the provided name (e.g., search in your database)
        //        List<ApplicationUser> searchedUsers = userRelationshipRepository.searchFollowers(name);
        //        return Json(searchedUsers);
        //    }
        //    else
        //    {
        //        // If the name is null or empty, return an empty list as JSON
        //        return Json(new List<ApplicationUser>());
        //    }
        //}
        //public ActionResult SearchFollower(string name)
        //{
        //    List<ApplicationUser> searchedUsers = userRelationshipRepository.searchFollowers(name);
        //    if (name != null)
        //    {

        //        return View("showFollowers", searchedUsers);
        //    }
        //    else
        //    {
        //        //List<ApplicationUser> searchedUsers = userRelationshipRepository.searchFollowers(name);
        //        //return View("showFollowers", searchedUsers);
        //        //return RedirectToAction("Index","Home");
        //        searchedUsers = new List<ApplicationUser>();
        //        return View("showFollowers", searchedUsers);

        //    }
        //}


        //public ActionResult SearchFollowee(string name)
        //{
        //    List<ApplicationUser> searchedUsers = userRelationshipRepository.searchFollowees(name);
        //    if (name != null)
        //    {

        //        return View("showFollowees", searchedUsers);
        //    }
        //    else
        //    {
        //        searchedUsers = new List<ApplicationUser>();
        //        return View("showFollowers", searchedUsers);

        //    }


        //}
        public IActionResult Profile()
        {
            return View("Profile");
        }

    }
}
