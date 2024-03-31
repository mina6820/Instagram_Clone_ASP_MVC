using Microsoft.AspNetCore.Mvc;

namespace Instagram_Clone.Controllers
{
    public class MarwaAndMessiTestController : Controller
    {
        public IActionResult Index()
        {
            return Content("success");
        }
    }
}
