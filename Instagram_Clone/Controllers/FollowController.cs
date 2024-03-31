using Instagram_Clone.Authentication;
using Instagram_Clone.Models;
using Instagram_Clone.Repositories.UserFollowRepo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public List<string> Autocomplete(string term)
        {
            var followerNames = context.UserRelationship
                .Where(ur => ur.Follower.UserName.Contains(term))
                .Select(ur => ur.Follower.UserName)
                .ToList();

            return followerNames;
        }
        

        /// follow / ShowAll
        //public IActionResult ShowAll()
        //{
        //    List<UserRelationship> lists = userRelationshipRepository.GetAll();
        //    return View("ShowAll",lists);
        //}



        //Follow/showFollowers?id=

        public IActionResult ShowFollowers(string Name)
        {
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;

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

            return View("ShowFollowers", profileUserViewModel);
        }

        //Follow/showFollowees?id=
        public ActionResult showFollowees()//string id)
        {
            //List<UserRelationship> Followees = userRelationshipRepository.GetFollowees(id);
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            //string name = User.Identity.Name;
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            profileUserViewModel.UserName = user.UserName;
            profileUserViewModel.FirstName = user.FirstName;
            profileUserViewModel.LastName = user.LastName;
            profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
            profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
            return View("showFollowees", profileUserViewModel);
        }
        public ActionResult SearchFollower( string name)
        {
            List<ApplicationUser> searchedUsers = userRelationshipRepository.searchFollowers(name);
            if (name != null)
            {
               
                return View("showFollowers", searchedUsers);
            }
            else
            {
                //List<ApplicationUser> searchedUsers = userRelationshipRepository.searchFollowers(name);
                //return View("showFollowers", searchedUsers);
                //return RedirectToAction("Index","Home");
                searchedUsers = new List<ApplicationUser>();
                return View("showFollowers", searchedUsers);

            } 
        }

        //
        //public ActionResult SearchFollowee(string name)
        //{
        //    List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowees(name);
        //    if (name != null)
        //    {

        //        return View("showFollowees", searchedUsers);
        //    }
        //    else
        //    {
        //        searchedUsers = new List<UserRelationship>();
        //        return View("showFollowers", searchedUsers);
        //
        //    }


        //}
        public IActionResult Profile()
        {
            return View("Profile");
        }

    }
}
