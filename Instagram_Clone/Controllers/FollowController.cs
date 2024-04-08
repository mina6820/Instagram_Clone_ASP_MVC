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

        //Abadeer

        //  /Follow/RemoveFollower?id=
        //public IActionResult RemoveFollower(string id)
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    if (user != null)
        //    {
        //        userRelationshipRepository.removeFollower(id);
        //        return RedirectToAction("ShowFollowers"); // Redirect to the action to refresh the followers list
        //    }
        //    return View("_FollowersList");
        //}

        ////  /Follow/RemoveFollowing?id=
        //public IActionResult unFollow(string id)
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    if (user != null)
        //    {
        //        userRelationshipRepository.removeFollowing(id);
        //        return RedirectToAction("showFollowees"); // Redirect to the action to refresh the following list
        //    }
        //    return View("_FollowingList");
        //}

        //public IActionResult RemoveFollower(string id)
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser currentUser = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    if (currentUser != null)
        //    {
        //        // Check if the relationship exists
        //        UserRelationship relationship = userRelationshipRepository.GetFollowerRelationship(currentUser.Id, id);
        //        if (relationship != null)
        //        {
        //            // Mark the relationship as deleted
        //            relationship.IsDeleted = true;
        //            context.SaveChanges();
        //            return RedirectToAction("ShowFollowers"); // Redirect to the action to refresh the followers list
        //        }
        //        else
        //        {
        //            // Handle the case where the relationship does not exist
        //            ModelState.AddModelError(string.Empty, "The follower relationship does not exist.");
        //            return View("_FollowersList");
        //        }
        //    }
        //    else
        //    {
        //        // Handle the case where the current user is not found
        //        ModelState.AddModelError(string.Empty, "User not found.");
        //        return View("_FollowersList");
        //    }
        //}

        //public IActionResult unFollow(string id)
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser currentUser = context.Users.FirstOrDefault(u => u.Id == claim.Value);

        //    if (currentUser != null)
        //    {
        //        // Check if the relationship exists
        //        UserRelationship relationship = userRelationshipRepository.GetFollowingRelationship(claim.Value, id);
        //        if (relationship != null)
        //        {
        //            // Mark the relationship as deleted
        //            relationship.IsDeleted = true;
        //            context.SaveChanges();
        //            return RedirectToAction("showFollowees"); // Redirect to the action to refresh the following list
        //        }
        //        else
        //        {
        //            // Handle the case where the relationship does not exist
        //            ModelState.AddModelError(string.Empty, "The followee relationship does not exist.");
        //            return View("_FollowingList");
        //        }
        //    }
        //    else
        //    {
        //        // Handle the case where the current user is not found
        //        ModelState.AddModelError(string.Empty, "User not found.");
        //        return View("_FollowingList");
        //    }
        //}

        public IActionResult UnFollow(string id)
        {
            // user.id (login user)
            // id (id of the follower)
            if (id != null)
            {
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
                userRelationshipRepository.GetFollowingRelationship(id,user2.Id);
             
            }
            return RedirectToAction("Index","Profile");
        }

        public IActionResult RemoveFollower(string id)
        {
            // user.id (login user)
            // id (id of the follower)
            if (id != null)

            {
                Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                ApplicationUser user2 = context.Users.FirstOrDefault(u => u.Id == claim.Value);
                userRelationshipRepository.GetFollowingRelationship(user2.Id,id);

            }
            return RedirectToAction("Index", "Profile");
        }



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




    }
}
