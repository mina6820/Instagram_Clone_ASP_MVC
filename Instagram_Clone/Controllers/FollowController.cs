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



        public ActionResult SearchFollower(string id , string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("showFollowers", id);
            }
            else
            {
                List<ApplicationUser> searched_Users = userRelationshipRepository.searchFollowers(id, name);

                return View("SearchFollower", searched_Users);

            } 
        }


        public ActionResult SearchFollowee(string id, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("showFollowees", id);
            }
            else
            {
                List<ApplicationUser> searched_Users = userRelationshipRepository.searchFollowees(id, name);

                return View("SearchFollowee", searched_Users);

            }


        }
        public IActionResult Profile()
        {
            return View("Profile");
        }

    }
}
