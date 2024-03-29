using Microsoft.AspNetCore.Mvc;

namespace Instagram_Clone.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
