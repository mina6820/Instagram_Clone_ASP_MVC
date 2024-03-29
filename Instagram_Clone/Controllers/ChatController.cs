using Microsoft.AspNetCore.Mvc;

namespace Instagram_Clone.Controllers
{
    public class ChatController: Controller
    {

        public IActionResult Index()
        {
            return View("Chat");
        }
    }
}
