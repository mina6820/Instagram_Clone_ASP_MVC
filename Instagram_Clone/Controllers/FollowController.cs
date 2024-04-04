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
        //public IActionResult AutocompleteSearch(string term)
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);

        //    // Replace this with your actual implementation to search for usernames based on the term
        //    List<string> userNames = SearchUserNames(term);

        //    return Json(userNames);
        //}

        public IActionResult ShowFollowers(string Name)
        {
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.ProfilePicture = user.ProfilePicture;

            if (Name == null)
            {
                profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
                profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            }
            else
            {
                profileUserViewModel.Followers = userRelationshipRepository.searchFollowers2(Name, user.Id);
                profileUserViewModel.Following = userRelationshipRepository.searchFollowees2(Name, user.Id);
            }

            return PartialView("_FollowersList", profileUserViewModel);
        }



        public IActionResult showFollowees(string Name)
        {
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.ProfilePicture = user.ProfilePicture;

            if (Name == null)
            {
                profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
                profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            }
            else
            {
                profileUserViewModel.Followers = userRelationshipRepository.searchFollowers2(Name, user.Id);
                profileUserViewModel.Following = userRelationshipRepository.searchFollowees2(Name, user.Id);
            }

            return PartialView("_FollowingList", profileUserViewModel);
        }


       

        public ActionResult SearchFollower(string name)
        {
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowers2(name,user.Id);
            if (name != null)
            {

                return PartialView("_FollowersList", searchedUsers);
            }
            else
            {
              
                searchedUsers = new List<UserRelationship>();
                return PartialView("_FollowersList", searchedUsers);

            }
        }
       

        public ActionResult SearchFollowee(string name)
        {
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowees2(name,user.Id);
            if (name != null)
            {

                return PartialView("_FollowingList", searchedUsers);
            }
            else
            {
                searchedUsers = new List<UserRelationship>();
                return PartialView("_FollowingList", searchedUsers);

            }
        }
        public IActionResult Profile(string id)
        {
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
    
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == id);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.Email = user.Email;
            profileUserViewModel.IsDeleted = user.IsDeleted;
            profileUserViewModel.ProfilePicture=user.ProfilePicture;    
            profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
            profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            return View("Profile" ,profileUserViewModel);
        }

    }
}
