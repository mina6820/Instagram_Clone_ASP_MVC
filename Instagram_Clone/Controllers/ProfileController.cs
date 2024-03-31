using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class ProfileController : Controller
    {
        private readonly Context context;

        public ProfileController(Context context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            string name = User.Identity.Name;
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            return View("Index",user);
        }
    }
}
