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
        //public ActionResult FollowersList(string id)
        //{
        //    List<ApplicationUser> Followers = userRelationshipRepository.GetFollowers(id);

        //    if(Followers == null)
        //    {
        //        return Content("hi");
        //    }

        //    return View("FollowersList",Followers);
        //}


        //public IActionResult Profile() 
        //{
        //    return View("Profile");
        //}

    }
}
