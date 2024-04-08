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

        //public IActionResult ShowFollowers(string Name)
        //{
        //    ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
           
        //    ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
        //    profileUserViewModel.UserName = user.UserName;
        //    profileUserViewModel.FirstName = user.FirstName;
        //    profileUserViewModel.LastName = user.LastName;
        //    profileUserViewModel.ProfilePicture = user.ProfilePicture;

        //    if (Name == null)
        //    {
        //        profileUserViewModel.Followers = userRelationshipRepository.GetFollowers(user.Id);
        //        profileUserViewModel.Following = userRelationshipRepository.GetFollowees(user.Id);
        //    }
        //    else
        //    {
        //        profileUserViewModel.Followers = userRelationshipRepository.searchFollowers2(Name, user.Id);
        //        profileUserViewModel.Following = userRelationshipRepository.searchFollowees2(Name, user.Id);
        //    }

        //    return PartialView("_FollowersList", profileUserViewModel);
        //}



       


       

        //public ActionResult SearchFollower(string name)
        //{
        //    //Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    //ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    //ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
        //    //List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowers2(name,user.Id);
        //    //if (name != null)
        //    //{

        //    //    return PartialView("_FollowersList", searchedUsers);
        //    //}
        //    //else
        //    //{

        //    //    searchedUsers = new List<UserRelationship>();
        //    //    return PartialView("_FollowersList", searchedUsers);

        //    //}
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

        //    // Search for followers based on the provided name
        //    List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowers2(name, user.Id);
        //    ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
        //    //{
        //    //    // Assign properties accordingly
        //    //    // For example:
        //    //    Followers = searchedUsers,
        //    //    // Following = ...
        //    //};
        //    if (name!=null)
        //    {
        //        // Convert the searched users to the appropriate view model
        //        profileUserViewModel.Followers = searchedUsers;
        //        return PartialView("_FollowersList", profileUserViewModel);

        //    }
        //    else
        //    {
        //        profileUserViewModel.Followers = new List<UserRelationship>();
        //        return PartialView("_FollowersList", profileUserViewModel);
        //    }


        //    // Return the PartialView with the searched users
        //}
       

     
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

        public IActionResult UnFollow(string id)
        {
            // user.id (login user)
            // id (id of the follower)
            if (id != null)
            {
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
                //ViewBag.LoggedInUserId= user2.Id;
                userRelationshipRepository.GetFollowingRelationship(id,user2.Id);
             
            }
            return RedirectToAction("Index","Profile");
        }

        public IActionResult RemoveFollower(string id)
        {
            // id (login user)
            //  user.id (id of the follower)
            if (id != null)

            {
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
                userRelationshipRepository.GetFollowingRelationship(user2.Id,id);

            }
            return RedirectToAction("Index", "Profile");
        }

        //public IActionResult GetFollowersAndFollowings()
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    List<UserRelationship> AllUsers = userRelationshipRepository.GetFollowersAndFollowings(user2.Id);
        //    return RedirectToAction("Index", "Home", AllUsers);
        //    //return PartialView("_DataBaseUsersPartial", AllUsers);
        //}

        //public IActionResult GetFollowersAndFollowings()
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    List<ApplicationUser> allUsers = userRelationshipRepository.GetFollowersAndFollowings(user.Id);
        //    return PartialView("_DataBaseUsersPartial", allUsers);
        //}

        //public IActionResult GetFollowersAndFollowings()
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    List<UserRelationship> AllUsers = userRelationshipRepository.GetFollowersAndFollowings(user2.Id);
        //    //return View("Index", AllUsers);
        //    List<UserRelationship> test = new List<UserRelationship>();
        //    test.Add(AllUsers.First());

        //    ViewBag.Users = test;
        //    return PartialView("_DataBaseUsersPartial");
        //}
        //habeba and messi 

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
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);

            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();

            if (name != null)
            {
                // Search for followers based on the provided name
                List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowers2(name, user.Id);
                profileUserViewModel.Followers = searchedUsers ?? new List<UserRelationship>();
            }
            else
            {
                profileUserViewModel.Followers = new List<UserRelationship>();
            }

            return  PartialView("_FollowersList", profileUserViewModel);
        }

        public ActionResult SearchFollowee(string name)
        {
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            ProfileUserViewModel profileUserViewModel = new ProfileUserViewModel();
            //ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            //ApplicationUser user = context.Users.Include(u => u.ProfilePicture).FirstOrDefault(u => u.Id == user2.Id);
            //List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowees2(name, user.Id);
            if (name != null)
            {
                List<UserRelationship> searchedUsers = userRelationshipRepository.searchFollowees2(name, user.Id);
                profileUserViewModel.Following = searchedUsers ?? new List<UserRelationship>();


               // return PartialView("_FollowingList", searchedUsers);
            }
            else
            {
                profileUserViewModel.Following = new List<UserRelationship>();

                //searchedUsers = new List<UserRelationship>();
                //return PartialView("_FollowingList", searchedUsers);
            }
            return PartialView("_FollowingList", profileUserViewModel);

        }

    }
}
