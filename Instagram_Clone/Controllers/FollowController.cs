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
        public IActionResult ShowAll()
        {
            List<UserRelationship> lists = userRelationshipRepository.GetAll();
            return View("ShowAll",lists);
        }
    }
}
