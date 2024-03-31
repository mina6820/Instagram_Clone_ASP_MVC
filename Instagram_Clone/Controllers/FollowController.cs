using Instagram_Clone.Authentication;
using Instagram_Clone.Repositories.UserFollowRepo;
using Microsoft.AspNetCore.Mvc;

namespace Instagram_Clone.Controllers
{
    public class FollowController : Controller
    {
       private IUserRelationshipRepository userRelationshipRepository;

        public FollowController( IUserRelationshipRepository _userRelationship )
        {
            userRelationshipRepository = _userRelationship;
        }

        /// follow / ShowAll
        //public IActionResult ShowAll()
        //{
        //    List<UserRelationship> lists = userRelationshipRepository.GetAll();
        //    return View("ShowAll",lists);
        //}



        //Follow/showFollowers?id=

        public ActionResult showFollowers(string id)
        {
            List<ApplicationUser> Followers = userRelationshipRepository.GetFollowers(id);

            return View("showFollowers", Followers);
        }

        //Follow/showFollowees?id=
        public ActionResult showFollowees(string id)
        {
            List<ApplicationUser> Followees = userRelationshipRepository.GetFollowees(id);

            return View("showFollowees", Followees);
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


        public ActionResult SearchFollowee(string name)
        {
            List<ApplicationUser> searchedUsers = userRelationshipRepository.searchFollowees(name);
            if (name != null)
            {

                return View("showFollowees", searchedUsers);
            }
            else
            {
                searchedUsers = new List<ApplicationUser>();
                return View("showFollowers", searchedUsers);

            }


        }
        public IActionResult Profile()
        {
            return View("Profile");
        }

    }
}
